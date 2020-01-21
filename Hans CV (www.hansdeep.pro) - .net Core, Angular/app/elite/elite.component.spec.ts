import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EliteComponent } from './elite.component';

describe('EliteComponent', () => {
  let component: EliteComponent;
  let fixture: ComponentFixture<EliteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EliteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EliteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
