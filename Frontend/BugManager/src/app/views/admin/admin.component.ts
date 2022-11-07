import { Component, OnInit } from '@angular/core';
import { SideBarItem } from 'src/app/components/nav/model/sidebarItem';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  adminMenuItems: SideBarItem[] = [
    { path: '/admin/bugs', title: 'Bugs', icon: 'bug_report' },
    { path: '/admin/add-user', title: 'Add User', icon: 'person_add' },
    { path: '/admin/projects', title: 'Projects', icon: 'list_alt' },
    { path: '/admin/assignments', title: 'Assignments', icon: 'assignment' },
    { path: '/admin/devs-scoreboard', title: 'Bugs Scoreboard', icon: 'check_circle_outline' },
    { path: '/admin/bug-import-classic', title: 'Bug Import Classic', icon: 'code' },
    { path: '/admin/bug-import-custom', title: 'Bug Import Custom', icon: 'upload' }
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
