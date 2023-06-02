import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit{
  

  @ViewChild('editForm') editForm: NgForm;
  @HostListener("window: beforeunload", ['$event']) unloadNotification($event: any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  member: Member;
  user: User;

  constructor(private as: AccountService, private ms: MembersService,
    private tostr: ToastrService){
    this.as.currentUser$.pipe(take(1)).subscribe(res=>{
      this.user = res;
    })
  }
  
  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    this.ms.getMember(this.user.userName).subscribe(res=>{
      this.member=res;
    })
  }

  updateMember(){
    this.ms.updateMember(this.member).subscribe(()=>{
      this.tostr.success("Profile updated successfully");
      this.editForm.reset(this.member);
    });
  }
}
