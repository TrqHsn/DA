import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  // currentUser$: Observable<User>;

  constructor(public accountService: AccountService) { }
  ngOnInit(): void {
    // this.currentUser$ = this.accountService.cureentUser$;
  }

  login() {
    this.accountService.login(this.model).subscribe(res => {
      console.log(res);
    }, error=>{
      console.log(error.error)
    });
  }



  logout(){
    this.accountService.logout();
  }
}