import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {MatOption} from '@angular/material/core';
import {MatFormField, MatSelect} from '@angular/material/select';
import {MatButton} from '@angular/material/button';
import {AppRole} from '../../../models/user.model';
import {MatLabel} from '@angular/material/form-field';
import {UserService} from '../../../services/user.service';
import {FormsModule} from '@angular/forms';
import {firstValueFrom} from 'rxjs';
import {AppComponent} from '../../../app.component';

@Component({
  selector: 'app-change-role',
  imports: [
    MatSelect,
    MatOption,
    MatButton,
    MatFormField,
    MatLabel,
    FormsModule
  ],
  templateUrl: './change-role.component.html',
  styleUrl: './change-role.component.scss'
})
export class ChangeRoleComponent {
  public selectedRole: string = '';

  constructor(
    private userService: UserService,
    private dialogRef: MatDialogRef<ChangeRoleComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { id: string, name: string }) { }

  public async updateUserRole() {
    const numericRole = Number(this.selectedRole.toString());

    if (!isNaN(numericRole)) {
      const roleEnum = numericRole as AppRole;

      await AppComponent.showLoadingFromPromise(firstValueFrom(this.userService.updateUserRole(this.data.id, roleEnum)));
      this.dialogRef.close();
    }
  }

  protected readonly AppRole = AppRole;
}
