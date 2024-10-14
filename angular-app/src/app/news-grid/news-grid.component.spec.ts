import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NewsGridComponent } from './news-grid.component';
import { NewsStoryService } from '../services/news-story.service';
import { delay, of } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('NewsGridComponent', () => {
  let component: NewsGridComponent;
  let fixture: ComponentFixture<NewsGridComponent>;
  let mockNewsStoryService: jasmine.SpyObj<NewsStoryService>;
  let mockSpinnerService: jasmine.SpyObj<NgxSpinnerService>;

  beforeEach(async () => {
    // Mock services.
    mockNewsStoryService = jasmine.createSpyObj('NewsStoryService', ['getNewestStories']);
    mockSpinnerService = jasmine.createSpyObj('NgxSpinnerService', ['show', 'hide']);

    await TestBed.configureTestingModule({
      declarations: [NewsGridComponent],
      providers: [
        { provide: NewsStoryService, useValue: mockNewsStoryService },
        { provide: NgxSpinnerService, useValue: mockSpinnerService }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewsGridComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should call NewsStoryService.getNewestStories() and populate rowData on init', () => {
    const mockStories = [{ title: 'Story 1', url: 'http://example.com/story1' }];

    // Mock the service to return our mock data.
    mockNewsStoryService.getNewestStories.and.returnValue(of(mockStories).pipe(delay(1000)));

    // Initialize the component.
    component.ngOnInit();
   
    // Check that the spinner is shown and hidden, service called, rowData has one entry with correct data.
    setTimeout(() => { 
      expect(mockSpinnerService.show).toHaveBeenCalled(); 
      expect(mockNewsStoryService.getNewestStories).toHaveBeenCalled();  
      expect(component.rowData.length).toBe(1);  
      expect(component.rowData[1]).toEqual(mockStories[1]);  
      expect(mockSpinnerService.hide).toHaveBeenCalled();  
      }, 1500);
  });

  it('should show and hide the spinner during the loading of data', () => {
    const mockStories = [{ title: 'Story 1', url: 'http://example.com/story1' }];
    
    // Mock the service to return our mock data
    mockNewsStoryService.getNewestStories.and.returnValue(of(mockStories).pipe(delay(1000)));

    // Initialize the component.
    component.ngOnInit();

    // Check that spinner was shown before data load.
    expect(mockSpinnerService.show).toHaveBeenCalled();

    // Check that spinner was hidden after data load.
    setTimeout(() => { expect(mockNewsStoryService.getNewestStories).toHaveBeenCalled();expect(component.rowData).toEqual(mockStories);  
      expect(mockSpinnerService.hide).toHaveBeenCalled();  
     }, 1500);
  });
});