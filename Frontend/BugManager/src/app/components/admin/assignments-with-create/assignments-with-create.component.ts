import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AssignmentService } from 'src/app/services/assignment.service';

@Component({
  selector: 'app-assignments-with-create',
  templateUrl: './assignments-with-create.component.html',
  styleUrls: ['./assignments-with-create.component.css']
})
export class AssignmentsWithCreateComponent implements OnInit {


  constructor(private router: Router, private r: ActivatedRoute, public dialog: MatDialog, private serviceAssignments: AssignmentService) { }

  ngOnInit(): void {
  }
  createAssignment() {
    this.router.navigate(["../assignment"], { relativeTo: this.r });
  }
}
