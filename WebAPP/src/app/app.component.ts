import { Component } from '@angular/core';
import { DemoService } from './services/demo.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'WebAPP';
  response: string = '';
  constructor(private demoService: DemoService) { }

  ngOnInit(): void {
    this.demoService.getData().subscribe(
      (data) => {
        console.log(data);
        this.response = data;
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }

}
