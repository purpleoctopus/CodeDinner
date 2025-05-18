import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSettingsPrivacyComponent } from './user-settings-privacy.component';

describe('UserSettingsPrivacyComponent', () => {
  let component: UserSettingsPrivacyComponent;
  let fixture: ComponentFixture<UserSettingsPrivacyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserSettingsPrivacyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserSettingsPrivacyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
