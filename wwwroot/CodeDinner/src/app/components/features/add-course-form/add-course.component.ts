import {Component, Inject} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CourseService} from '../../../services/features/course.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-add-course-form',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './add-course-form.component.html',
  styleUrl: './add-course-form.component.scss'
})
export class AddCourseComponent {

  constructor(
    private dialogRef: MatDialogRef<AddCourseComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { message: string },
    private courseService: CourseService) { }

  form = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    language: new FormControl('', [Validators.required]),
  });

  public async submit(){
    if(this.form.valid){
      const res = await this.courseService.createCourse({
        name: this.form.value.name ?? '',
        language: Number(this.form.value.language) ?? 0
      });
      this.dialogRef.close(res);
    }
  }
}
