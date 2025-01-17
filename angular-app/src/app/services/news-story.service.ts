import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})

export class NewsStoryService {
  private apiUrl = environment.apiURL + '/NewsStory';

  constructor(private http: HttpClient){}

  public getNewestStories(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
