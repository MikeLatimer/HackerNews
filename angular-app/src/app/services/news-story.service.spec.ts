import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { NewsStoryService } from './news-story.service';
import { environment } from '../../environments/environment.development';
import { provideHttpClient } from '@angular/common/http';

describe('NewsStoryService', () => {
  let service: NewsStoryService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({ 
      providers: [NewsStoryService, provideHttpClient(), provideHttpClientTesting()]      
    });

    // Inject the service and the testing controller.
    service = TestBed.inject(NewsStoryService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    // Verifies that no unmatched requests remain.
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call get and return data', () => {
    // Mock data.
    const mockResponse = [{ title: 'Test Story' }]; 

    // Call the service's get method.
    service.getNewestStories().subscribe((data) => {
      expect(data).toEqual(mockResponse);
    });

    // Expect that an HTTP request has been made.
    const req = httpTestingController.expectOne(`${environment.apiURL}/NewsStory`);

    // Ensure the request was a GET.
    expect(req.request.method).toEqual('GET');

    // Respond with the mock data.
    req.flush(mockResponse);
  });
});