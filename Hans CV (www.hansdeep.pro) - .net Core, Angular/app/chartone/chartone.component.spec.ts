import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartoneComponent } from './chartone.component';

describe('ChartoneComponent', () => {
  let component: ChartoneComponent;
  let fixture: ComponentFixture<ChartoneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartoneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
