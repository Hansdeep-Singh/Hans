import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PolarismenuComponent } from './polarismenu.component';

describe('PolarismenuComponent', () => {
  let component: PolarismenuComponent;
  let fixture: ComponentFixture<PolarismenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PolarismenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PolarismenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
