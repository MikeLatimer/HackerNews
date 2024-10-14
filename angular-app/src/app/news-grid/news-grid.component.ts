import { Component, OnInit } from '@angular/core';
import { NewsStoryService } from '../services/news-story.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { delay, finalize } from 'rxjs';

@Component({
  selector: 'app-news-grid',
  templateUrl: './news-grid.component.html',
  styleUrl: './news-grid.component.css'
})

export class NewsGridComponent implements OnInit { 
  columnDefs = [
    { headerName: 'Title', field: 'title', sortable: true, filter: true, flex: 2 },
    { headerName: 'URL', field: 'url', cellRenderer: this.urlCellRenderer, sortable: true, filter: true, flex: 2 }
  ];
  
  rowData: any[] = [];

  constructor(private newsStoryService: NewsStoryService, private spinner: NgxSpinnerService){}

  ngOnInit(): void {
    this.spinner.show();
  
    this.newsStoryService.getNewestStories()
      .pipe(
        delay(1000),
        finalize(() => this.spinner.hide())
      )
      .subscribe({
        next: (stories) => {
          this.rowData = stories; 
        },
        error: (err) => {
          console.error('Error loading stories', err); 
        }
      });
  }

  // Custom ag-grid cell renderer to display clickable URLs.
  urlCellRenderer(params: any) {
    return `<a href="${params.value}" target="_blank">${params.value}</a>`;
  }
}

