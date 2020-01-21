import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartthreeComponent } from './chartthree.component';

describe('ChartthreeComponent', () => {
  let component: ChartthreeComponent;
  let fixture: ComponentFixture<ChartthreeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartthreeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartthreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
