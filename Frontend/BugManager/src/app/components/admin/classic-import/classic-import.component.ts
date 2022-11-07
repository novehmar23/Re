import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ClassicImportService } from 'src/app/services/classic-import.service';
import { InfoMessage } from '../../message/model/message';

@Component({
  selector: 'app-classic-import',
  templateUrl: './classic-import.component.html',
  styleUrls: ['./classic-import.component.css']
})
export class ClassicImportComponent implements OnInit {

  showCancelButton = false;
  loading = false;
  infoMessage: InfoMessage = { error: true, text: '' };
  form = new FormGroup({
    path: new FormControl('', [Validators.required]),
  });
  path;
  constructor(private service: ClassicImportService) { }

  ngOnInit(): void {
  }

  save(): void {
    this.loading = true;
    this.service.importBugs(this.path).subscribe(

      (response: any) => {
        this.loading = false;
        this.infoMessage.error = false;
        this.infoMessage.text = `Bugs  imported successfully`
        this.form.reset();
        this.form.markAsUntouched();

      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error
      }
    );
  }
}
