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
  @ViewChild('username') usernameInput!: ElementRef;
  @Input('user') user: UserDetail | null = null;
  protected userForm!: FormGroup;
  private autoSaveSub!: Subscription;

  constructor(private userService: UserService, private fb: FormBuilder) {}

  ngOnInit() {
    this.userForm = this.fb.group({
      id: [this.user?.id || ''],
      username: [{value: this.user?.username || '', disabled: true}],
      firstName: [this.user?.firstName || ''],
      lastName: [this.user?.lastName || ''],
    });
    this.autoSaveSub = this.userForm.valueChanges.pipe(debounceTime(1500), filter(() => this.userForm.valid))
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
      AppComponent.showMessage()
    }
    return res;
  }
}
