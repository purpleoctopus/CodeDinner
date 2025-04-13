import {Component, Inject} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {MatOption} from '@angular/material/core';
import {Store} from '@ngrx/store';
import {login} from '../../../store/auth/actions';

@Component({
  selector: 'app-login-form',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss'
})
export class LoginFormComponent {

  constructor(
    private dialogRef: MatDialogRef<LoginFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { message: string },
    private store: Store) { }

  form = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  public async submit(){
    if(this.form.valid){
      const { username, password } : any = this.form.value;
      this.store.dispatch(login({ username, password }));
      this.dialogRef.close();
    }
  }
}
