import {Component, ElementRef, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {UserDetail} from '../../../models/user.model';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {UserService} from '../../../services/user.service';
import {debounceTime, filter, firstValueFrom, Subscription} from 'rxjs';
import {AppComponent} from '../../../app.component';

@Component({
  selector: 'app-user-settings-general',
  imports: [
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './user-settings-general.component.html',
  styleUrl: './user-settings-general.component.scss'
})
export class UserSettingsGeneralComponent implements OnInit, OnDestroy {
  @ViewChild('username') usernameInput!: ElementRef;
  @Input('user') user: UserDetail | null = null;
  protected formGroup!: FormGroup;
  private autoSaveSub!: Subscription;

  constructor(private userService: UserService, private fb: FormBuilder) {}

  ngOnInit() {
    this.formGroup = this.fb.group({
      id: [this.user?.id || ''],
      username: [{value: this.user?.username || '', disabled: true}]
    });
    this.autoSaveSub = this.formGroup.valueChanges.pipe(debounceTime(1500), filter(() => this.formGroup.valid))
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
    const value = this.formGroup.getRawValue();
    this.usernameInput.nativeElement.disabled = true;
    const res = await firstValueFrom(this.userService.updateMyUser(value));
    if(res.success){
      AppComponent.showMessage()
    }
    return res;
  }
}
