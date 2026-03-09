import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DevolverEjemplarComponent } from './devolver-ejemplar.component';

describe('DevolverEjemplarComponent', () => {
  let component: DevolverEjemplarComponent;
  let fixture: ComponentFixture<DevolverEjemplarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DevolverEjemplarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DevolverEjemplarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
