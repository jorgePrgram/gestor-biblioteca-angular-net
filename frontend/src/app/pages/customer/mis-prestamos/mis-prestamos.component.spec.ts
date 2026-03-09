import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MisPrestamosComponent } from './mis-prestamos.component';

describe('MisPrestamosComponent', () => {
  let component: MisPrestamosComponent;
  let fixture: ComponentFixture<MisPrestamosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MisPrestamosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MisPrestamosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
