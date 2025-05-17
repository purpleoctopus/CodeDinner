import {Component, Inject} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {AuthService} from '../../../services/auth.service';
import {ApiResponse, ResponseError} from '../../../models/response.model';
import {SessionModel} from '../../../models/session.model';

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
  public invalidCredentials = false;

  constructor(
    private dialogRef: MatDialogRef<LoginFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { message: string },
    private authService: AuthService) { }

  form = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  public async submit() {
    this.invalidCredentials = false;
    if(this.form.valid){
      const { username, password } : any = this.form.value;
      let res: ApiResponse<SessionModel>;
      try {
        res = await this.authService.login({username, password})
      }catch(err: any){
        console.log(err)
        res = err.error;
      }
      if(res.success){
        this.dialogRef.close(res)
      }else{
        if(res.errors?.includes(ResponseError.InvalidCredentials)){
          this.invalidCredentials = true;
        }
      }
    }
  }
}
