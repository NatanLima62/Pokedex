import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SnackbarErrorComponent } from './snackbar-error.component';

describe('ErroComponent', () => {
  let component: SnackbarErrorComponent;
  let fixture: ComponentFixture<SnackbarErrorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SnackbarErrorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SnackbarErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
