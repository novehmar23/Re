import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ImporterInfo } from 'src/app/models/CustomImporter/ImporterInfo';
import { ParamType } from 'src/app/models/CustomImporter/ParamType';
import { CustomImportService } from 'src/app/services/custom-import.service';
import { InfoMessage } from '../../message/model/message';


@Component({
  selector: 'app-custom-import',
  templateUrl: './custom-import.component.html',
  styleUrls: ['./custom-import.component.css']
})


export class CustomImportComponent implements OnInit {

  showCancelButton = false;
  loading = false;
  infoMessage: InfoMessage = { error: true, text: '' };
  form = new FormGroup({});

  selectedImporterControl = new FormControl('', Validators.required);
  importers: ImporterInfo[] = [];
  importer: ImporterInfo;
  paramTypes = ParamType;
  constructor(private service: CustomImportService) {
  }

  ngOnInit(): void {
    this.getImportersInfo();
  }

  updateSelectedImported() {
    this.infoMessage.text = '';
    this.importer = this.selectedImporterControl.value;
    if (this.importer == null) {
      this.form.setErrors({ 'invalid': true })
      return;
    }
    this.form.setErrors(null)

    this.form = new FormGroup({});

    this.importer.params.forEach(param => {
      this.form.addControl(param.name, new FormControl('', [Validators.required]));
    });

  }

  getImportersInfo() {
    this.loading = true;
    this.service.getImportersInfo().subscribe(

      (response: ImporterInfo[]) => {
        this.loading = false;
        this.importers = response;
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error
      }
    );
  }

  save(): void {
    this.loading = true;
    this.convertValuesToString();
    this.service.importBugs(this.importer).subscribe(

      (response: any) => {
        this.loading = false;
        this.infoMessage.error = false;
        this.infoMessage.text = `Bugs imported successfully`
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

  convertValuesToString() {
    this.importer.params.forEach(param => {
      if (param.value == undefined)
        param.value = String(param.valueOriginType);
    })
  }
}
