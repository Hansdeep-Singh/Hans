import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartfourComponent } from './chartfour.component';

describe('ChartfourComponent', () => {
  let component: ChartfourComponent;
  let fixture: ComponentFixture<ChartfourComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartfourComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartfourComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
