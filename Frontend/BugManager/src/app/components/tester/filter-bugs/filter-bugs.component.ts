import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Bug } from 'src/app/models/Bug';

interface Filters {
  id?: number,
  projectName?: string,
  bugName?: string,
  status?: boolean,
}

@Component({
  selector: 'app-filter-bugs',
  templateUrl: './filter-bugs.component.html',
  styleUrls: ['./filter-bugs.component.css']
})
export class FilterBugsComponent implements OnInit {

  dataSource: Bug[];
  allBugs: Bug[];
  filteredBugs: Bug[];

  filters: Filters = {};

  selectionOptions = [
    { value: null, viewValue: "Any" },
    { value: false, viewValue: "Resolved" },
    { value: true, viewValue: "Unresolved" },
  ]
  selectedOption = this.selectionOptions[0].value;

  form = new FormGroup({
    id: new FormControl('', []),
    projectName: new FormControl('', []),
    bugName: new FormControl('', []),
    state: new FormControl('', []),
  });

  constructor() {
  }

  ngOnInit(): void {
  }

  sendBugs(dataSource) {
    this.allBugs = dataSource;
    this.filteredBugs = this.allBugs;
  }

  applyFilter() {
    let tempFilteredBug: Bug[] = [];
    tempFilteredBug = this.allBugs;
    if (this.filters.id != undefined)
      tempFilteredBug = this.filteredBugs.filter(b => b.id == this.filters.id);

    tempFilteredBug = tempFilteredBug.filter(b => b.projectName.toLowerCase().includes(this.filters.projectName?.toLowerCase() || ""));
    tempFilteredBug = tempFilteredBug.filter(b => b.name.toLowerCase().includes(this.filters.bugName?.toLowerCase() || ""));

    if (this.filters.status != undefined)
      tempFilteredBug = tempFilteredBug.filter(b => b.isActive == this.filters.status);

    this.dataSource = tempFilteredBug;
  }

  clearFilter() {
    this.filters = {};
    this.dataSource = this.allBugs;
  }

}
