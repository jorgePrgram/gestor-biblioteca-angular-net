import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AceptarPrestamosComponent } from './aceptar-prestamos.component';

describe('AceptarPrestamosComponent', () => {
  let component: AceptarPrestamosComponent;
  let fixture: ComponentFixture<AceptarPrestamosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AceptarPrestamosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AceptarPrestamosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
