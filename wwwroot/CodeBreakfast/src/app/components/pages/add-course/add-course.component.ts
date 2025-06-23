import { Component } from '@angular/core';
import {FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { Language } from '../../../models/course.model';
import {MatFormField, MatInput} from '@angular/material/input';
import {MatChip} from '@angular/material/chips';
import {MatLabel} from '@angular/material/form-field';
import {MatIcon} from '@angular/material/icon';
import {MatButton, MatIconButton} from '@angular/material/button';
import {NgForOf} from '@angular/common';
import {MatSelect} from '@angular/material/select';
import {MatOption} from '@angular/material/core';
import {CourseService} from '../../../services/course.service';
import {firstValueFrom} from 'rxjs';
import {AppComponent} from '../../../app.component';

@Component({
  selector: 'app-add-course',
  imports: [
    MatFormField,
    MatLabel,
    MatInput,
    MatIcon,
    MatChip,
    MatButton,
    NgForOf,
    MatSelect,
    MatOption,
    ReactiveFormsModule,
    MatIconButton,
  ],
  templateUrl: './add-course.component.html',
  styleUrl: './add-course.component.scss'
})
export class AddCourseComponent {
  form: FormGroup;
  Language = Language;
  languageOptions = Object.keys(Language).filter(k => !isNaN(Number(k)));

  constructor(private fb: FormBuilder, private courseService: CourseService) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      language: [Language.Ukrainian, Validators.required],
      primarySpecialization: ['', Validators.required],
      tags: this.fb.array([])
    });
  }

  get tags(): FormArray {
    return this.form.get('tags') as FormArray;
  }

  addTag(tag: string) {
    if (tag.trim()) {
      this.tags.push(this.fb.control(tag.trim()));
    }
  }

  removeTag(index: number) {
    this.tags.removeAt(index);
  }

  async submit() {
    if (this.form.valid) {
      const value = this.form.value;
      const res = await firstValueFrom(this.courseService.createCourse(value));
      if(res?.success){
        AppComponent.showMessage('Курс успішно створений!');
      }else{
        AppComponent.showMessage('Щось пішло не так...', false)
      }
    }
  }

  protected readonly Number = Number;
}
