import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {UserService} from '../../../services/user.service';
import {LegendPosition, PieChartModule} from '@swimlane/ngx-charts';
import {BehaviorSubject, firstValueFrom, map, Observable, Subject, throttleTime} from 'rxjs';
import {AsyncPipe} from '@angular/common';
import {MatIcon} from '@angular/material/icon';
import {ConfigKeySectionVisibility, UserConfigDetailDto, UserConfigKey, UserConfigUpdateDto} from '../../../models/user-config.model';
import {MatIconButton} from '@angular/material/button';
import {UserConfigService} from '../../../services/user-config.service';
import {MatTooltip} from '@angular/material/tooltip';
import {RouterLink} from '@angular/router';
import {UserProfile} from '../../../models/user.model';
import {AppComponent} from '../../../app.component';

@Component({
  selector: 'app-my-profile',
  imports: [
    PieChartModule,
    AsyncPipe,
    MatIconButton,
    MatIcon,
    MatTooltip,
    RouterLink,
  ],
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.scss'
})
export class MyProfileComponent implements OnInit {
  @ViewChild('profile_picture') profilePictureElem!: ElementRef;

  private readonly weeksCount = 15;

  private sectionsVisibilityQueue = new Subject<ConfigKeySectionVisibility>();
  protected sectionsVisibility: UserConfigUpdateDto<boolean>[] = [];

  protected user: UserProfile | null = null;

  protected courses = [
    {name: 'a', value: 3000},
    {name: 'b', value: 300}
  ]

  private userLastActivity: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);
  public userLastActivity$: Observable<any[]> = this.userLastActivity.asObservable();

  constructor(private userService: UserService, private userConfigService: UserConfigService) {}

  ngOnInit(): void {
    this.sectionsVisibilityQueue.pipe(throttleTime(200)).subscribe(key => {
      this.executeChangeSectionVisibility(key);
    });

    AppComponent.showLoadingFromPromise(this.load());
  }

  public async ChangeSectionVisibility(key: ConfigKeySectionVisibility){
    this.sectionsVisibilityQueue.next(key);
  }

  private async executeChangeSectionVisibility(key: ConfigKeySectionVisibility) {
    let userConfig = this.sectionsVisibility.find(x => x.key === key);

    if (!userConfig) {
      userConfig = { key, value: true };
    } else {
      userConfig.value = !userConfig.value;
    }

    const res = await firstValueFrom(
      this.userConfigService.updateUserConfigs([{ key: userConfig.key, value: userConfig.value.toString() }])
    );

    if (res.success && res.data) {
      const userConfigs = res.data.map((x: UserConfigDetailDto<boolean | string>) => ({
        ...x,
        value: x.value === "true"
      }));

      this.sectionsVisibility = [...this.sectionsVisibility, ...userConfigs.filter(x => x.key >= 100)];
    }
  }

  public sectionVisibility(key: ConfigKeySectionVisibility){
    return this.sectionsVisibility.find(x=>x.key === key)?.value;
  }

  private async load(){
    await this.getUserProfile();
    await this.getUserConfigs();
    await this.getProfilePicture();
  }

  private async getUserConfigs(){
    const response = await firstValueFrom(this.userConfigService.getUserConfigs());

    if(!response.success || !response.data){
      return;
    }

    this.sectionsVisibility = response.data.map(x => {
      return {key: x.key, value: x.value === "true"}
    }) ?? [];
  }

  private async getProfilePicture(){
    const response = await firstValueFrom(this.userService.getMyProfilePicture());
    const imgElem = this.profilePictureElem.nativeElement as HTMLImageElement;

    if(response.success && response.data){
      imgElem.src = 'data:image/jpeg;base64,' + response.data;
    }else{
      imgElem.src = "images/empty-profile-image.png"
    }
  }

  private async getUserProfile(){
    const response = await firstValueFrom(this.userService.getMyProfile());

    if (!response.success) {
      return;
    }

    this.user = response.data;
  }

  protected async updateUserProfilePicture(event: Event) {
    if(event.target === null){
      return;
    }
    const input = event.target as HTMLInputElement;
    const file: File | null = input.files?.[0] ?? null;

    if (file) {
      const formData = new FormData();
      formData.append('file', file);

      const res = await AppComponent.showLoadingFromPromise(firstValueFrom(this.userService.uploadMyProfilePicture(formData)));
      if (res.success) {
        AppComponent.showMessage('Успіх!');
        await this.getProfilePicture();
      }else{
        AppComponent.showMessage('Помилка!', false)
      }
    }
  }

  protected readonly LegendPosition = LegendPosition;
  protected readonly UserConfigKey = UserConfigKey;
}
