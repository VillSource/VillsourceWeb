import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  public name: string = '';
  public version: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
    this.getInfo();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/api/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getInfo() {
    this.name$.subscribe(n => this.name = n.value);
    this.version$.subscribe(v => this.version = v.value);
  }
  get version$() {
    return this.http.get('api/version').pipe(tap(console.log));
  }

  get name$() {
    return this.http.get('api/name').pipe(tap(console.log));
  }

  title = 'villsource.client';
}
