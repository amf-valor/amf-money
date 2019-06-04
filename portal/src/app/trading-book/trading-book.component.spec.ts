import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TradingBookComponent } from './trading-book.component';
import { DebugElement } from '@angular/core';
import { By } from '@angular/platform-browser';

describe('TradingBookComponent', () => {
  let component: TradingBookComponent;
  let fixture: ComponentFixture<TradingBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TradingBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TradingBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  //TODO: checar esse teste
  // it('should not render create button if trading books is not empty', () => {
  //   component.tradingBooks = [1,2];
  //   const debugElement: DebugElement = fixture.debugElement;
  //   expect(debugElement.query(By.css('.test')).nativeElement).toBeFalsy();
  // });

  it('should render create button if trading books is empty', () => {
    component.tradingBooks = [];
    const tradingBookHe: HTMLElement = fixture.debugElement.nativeElement;
    const createButton = tradingBookHe.querySelector('button');
    expect(createButton).toBeTruthy();
  });
});
