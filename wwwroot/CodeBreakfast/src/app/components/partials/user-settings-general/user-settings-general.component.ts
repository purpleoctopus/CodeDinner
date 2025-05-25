import {Component, ElementRef, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {UserDetail} from '../../../models/user.model';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {UserService} from '../../../services/user.service';
import {debounceTime, filter, firstValueFrom, Subscription} from 'rxjs';
import {AppComponent} from '../../../app.component';
import {MatFormField, MatInput} from '@angular/material/input';

@Component({
  selector: 'app-user-settings-general',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatFormField,
    MatInput,
  ],
  templateUrl: './user-settings-general.component.html',
  styleUrl: './user-settings-general.component.scss'
})
export class UserSettingsGeneralComponent implements OnInit, OnDestroy {
  @ViewChild('profile_picture') profilePictureElem!: ElementRef;
  @ViewChild('username') usernameInput!: ElementRef;
  @Input('user') user: UserDetail | null = null;
  protected userForm!: FormGroup;
  private autoSaveSub!: Subscription;

  constructor(private userService: UserService, private fb: FormBuilder) {}

  ngOnInit() {
    AppComponent.showLoadingFromPromise(this.getProfilePicture());
    this.userForm = this.fb.group({
      id: [this.user?.id || ''],
      username: [{value: this.user?.username || '', disabled: true}],
      firstName: [this.user?.firstName || ''],
      lastName: [this.user?.lastName || ''],
    });
    this.autoSaveSub = this.userForm.valueChanges.pipe(debounceTime(1000), filter(() => this.userForm.valid))
    .subscribe(() => {
      this.save()
    })
  }

  ngOnDestroy(): void {
    this.autoSaveSub.unsubscribe();
  }

  enableUsernameEditing() {
    this.usernameInput.nativeElement.disabled = false;
    this.usernameInput.nativeElement.focus();
  }

  async save(){
    const value = this.userForm.getRawValue();
    this.usernameInput.nativeElement.disabled = true;
    const res = await firstValueFrom(this.userService.updateMyUser(value));
    if(res.success){
      AppComponent.showMessage('Збережено!')
    }
    return res;
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
        AppComponent.showMessage('Збережено!');
        await this.getProfilePicture();
      }else{
        AppComponent.showMessage('Помилка!', false)
      }
    }
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
}
