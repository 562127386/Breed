import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { HerdServiceProxy, HerdListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import * as moment from 'moment';

declare var Stimulsoft: any;
Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OFPaZYasFtsxNoDemsFOXbvf7SIcnyAkFX/4u37NTfx7g+0IqLXw6QIPolr1PvCSZz8Z5wjBNakeCVozGGOiuCOQDy60XNqfbgrOjxgQ5y/u54K4g7R/xuWmpdx5OMAbUbcy3WbhPCbJJYTI5Hg8C/gsbHSnC2EeOCuyA9ImrNyjsUHkLEh9y4WoRw7lRIc1x+dli8jSJxt9C+NYVUIqK7MEeCmmVyFEGN8mNnqZp4vTe98kxAr4dWSmhcQahHGuFBhKQLlVOdlJ/OT+WPX1zS2UmnkTrxun+FWpCC5bLDlwhlslxtyaN9pV3sRLO6KXM88ZkefRrH21DdR+4j79HA7VLTAsebI79t9nMgmXJ5hB1JKcJMUAgWpxT7C7JUGcWCPIG10NuCd9XQ7H4ykQ4Ve6J2LuNo9SbvP6jPwdfQJB6fJBnKg4mtNuLMlQ4pnXDc+wJmqgw25NfHpFmrZYACZOtLEJoPtMWxxwDzZEYYfT";


@Component({
    selector: 'herdCertificateReport',
    templateUrl: './herdCertificateReport.component.html'
})
export class HerdCertificateReportComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    herd: HerdListDto = new HerdListDto();  

    active: boolean = false;
    options: any;
    designer: any;
    viewer: any;
    report: any;
    dataSet: any;

    constructor(
        injector: Injector,
        private _herdService: HerdServiceProxy
    ) {
        super(injector);
    }

    show(herd: HerdListDto): void {  

        
        this._herdService.getHerdCertificated(herd.id).subscribe(result => {
            this.options = new Stimulsoft.Viewer.StiViewerOptions();
            this.options.appearance.rightToLeft = false;
            this.options.toolbar.showOpenButton = false;
            this.options.toolbar.showBookmarksButton = false;
            this.options.toolbar.showParametersButton = false;
            this.options.toolbar.showResourcesButton = false;
            this.options.toolbar.showEditorButton = false;
            this.options.toolbar.showFullScreenButton = false;
            this.options.toolbar.showViewModeButton = false;
            this.options.toolbar.showDesignButton = false;
            this.options.toolbar.showAboutButton = false;
            this.options.toolbar.printDestination = Stimulsoft.Viewer.StiPrintDestination.Default;

            this.report = new Stimulsoft.Report.StiReport(); 
            this.report.loadFile("/assets/reports/HerdCertificate.mrt");

            this.dataSet = new Stimulsoft.System.Data.DataSet("Breed");
            this.dataSet.readJson(result);

            this.report.dictionary.databases.clear();
            this.report.regData(this.dataSet.dataSetName, this.dataSet.dataSetName, this.dataSet);

            this.viewer = new Stimulsoft.Viewer.StiViewer(this.options, 'StiViewer', false);                                
            this.viewer.report = this.report;

            this.active = true;
            this.modal.show();   

            
        });
        
    }

    onShown(): void {
        this.viewer.renderHtml('viewer');
    }      

    close(): void {
        this.active = false;
        this.modal.hide();
    }    

}
