import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSettingsGeneralComponent } from './user-settings-general.component';

describe('UserSettingsGeneralComponent', () => {
  let component: UserSettingsGeneralComponent;
  let fixture: ComponentFixture<UserSettingsGeneralComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserSettingsGeneralComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserSettingsGeneralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
