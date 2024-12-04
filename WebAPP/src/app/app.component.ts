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
  menuItems: any[] | undefined;

  constructor(private demoService: DemoService) { }

  ngOnInit(): void {
    this.menuItems = [
      { label: 'Home', icon: 'pi pi-home', routerLink: ['/'] },
      { label: 'Sign In', icon: 'pi pi-sign-in', routerLink: ['/signin'] },
      { label: 'User Dashboard', icon: 'pi pi-user', routerLink: ['/user'] },
      { label: 'Sign Out', icon: 'pi pi-sign-out', routerLink: ['/signout'] },
    ];
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
