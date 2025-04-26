import {Component, Inject} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {AuthService} from '../../../services/auth.service';
import {firstValueFrom} from 'rxjs';

@Component({
  selector: 'app-register-form',
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
    private authService: AuthService) { }

  form = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),
  });

  public async submit(){
    if(this.form.valid){
      const { username, password } : any = this.form.value;
      const res = await firstValueFrom(this.authService.register({username, password}));
      if(res.success) {
        this.dialogRef.close();
      }
    }
  }
}
