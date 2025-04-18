import {Component, Inject} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {MatOption} from '@angular/material/core';
import {Store} from '@ngrx/store';
import {login, register} from '../../../store/auth/actions';

@Component({
  selector: 'app-login-form',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.scss'
})
export class RegisterFormComponent {

  constructor(
    private dialogRef: MatDialogRef<RegisterFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { message: string },
    private store: Store) { }

  form = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),
  });

  public async submit(){
    if(this.form.valid){
      const { username, password } : any = this.form.value;
      this.store.dispatch(register({ username, password }));
      this.dialogRef.close();
    }
  }
}
