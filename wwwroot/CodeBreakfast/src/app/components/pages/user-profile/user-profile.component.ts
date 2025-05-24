import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {AsyncPipe} from "@angular/common";
import {LegendPosition, PieChartModule} from '@swimlane/ngx-charts';
import {BehaviorSubject, firstValueFrom, Observable} from 'rxjs';
import {ConfigKeySectionVisibility,  UserConfigUpdateDto} from '../../../models/user-config.model';
import {UserProfile, UserProfileSection} from '../../../models/user.model';
import {UserService} from '../../../services/user.service';
import {UserConfigService} from '../../../services/user-config.service';
import {AppComponent} from '../../../app.component';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-user-profile',
  imports: [
      AsyncPipe,
      PieChartModule,
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent implements OnInit {
  @ViewChild('profile_picture') profilePictureElem!: ElementRef;

  private readonly weeksCount = 15;

  protected sectionsVisibility: UserConfigUpdateDto<boolean>[] = [];

  protected user: UserProfile | null = null;

  protected courses = [
    {name: 'a', value: 3000},
    {name: 'b', value: 300}
  ]

  private userLastActivity: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);
  public userLastActivity$: Observable<any[]> = this.userLastActivity.asObservable();

  constructor(private userService: UserService, private userConfigService: UserConfigService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    AppComponent.showLoadingFromPromise(this.load());
  }

  private async load(){
    const userId: string = this.activatedRoute.snapshot.params['id'];
    await this.getUserProfile(userId);
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

  private async getUserProfile(id: string){
    const response = await firstValueFrom(this.userService.getUserProfile(id));
    console.log(response);

    if (!response.success) {
      return;
    }

    this.user = response.data;
  }

  protected readonly LegendPosition = LegendPosition;
  protected readonly UserProfileSection = UserProfileSection;
}
