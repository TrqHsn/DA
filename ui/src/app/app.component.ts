import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'DA';
  users: any;
  baseUrl: string = 'https://localhost:5000';

  constructor(private http: HttpClient){}
  
  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.http.get("https://localhost:5000/users").subscribe(res=>{
      this.users=res; 
    }, error=>{
        console.log(error);
      })
  }
}
