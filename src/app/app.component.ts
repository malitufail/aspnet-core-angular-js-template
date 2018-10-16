import { Component } from '@angular/core';
import { AppService } from './app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  items: string[];
  constructor(private appService: AppService) {
    this.items = [];
    this.populateItems();
  }
  populateItems() {
    this.appService.getItems().subscribe(res => {
      this.items = res;
    });
  }
}
