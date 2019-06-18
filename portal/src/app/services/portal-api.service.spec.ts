import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PortalApiService } from './portal-api.service';
import { TradingBookSettings } from '../trading-books/trading-book-settings/trading-book-settings.model';
import { environment } from 'src/environments/environment';

describe('ApiPortalService', () => {
  let injector: TestBed
  let service: PortalApiService 
  let httpMock: HttpTestingController
  
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PortalApiService]
    })
    injector = getTestBed()
    service = injector.get(PortalApiService) 
    httpMock = injector.get(HttpTestingController)
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should create new trading book', () => {
    const settings: TradingBookSettings = { name: "bookTest", amountPerCaptal: 15, riskRewardRatio: 3}
    
    service.createTradingBook(settings).subscribe(tradingBook =>{
      expect(tradingBook.settings.name).toEqual(settings.name)
      expect(tradingBook.settings.amountPerCaptal).toEqual(0.15)
      expect(tradingBook.settings.riskRewardRatio).toEqual(settings.riskRewardRatio)
    })

    const req = httpMock.expectOne(`${environment.PORTAL_API_ADDRESS}/tradingBooks`)
    expect(req.request.method).toBe('POST')
    req.flush(null, {status: 200, statusText: 'Ok'});
  });

  afterEach(() => {
    httpMock.verify();
  });
});
