import { Component, Injector, ViewChild, ViewEncapsulation, ElementRef, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { HerdServiceProxy, ReportHerdCertificatedOutput } from '@shared/service-proxies/service-proxies';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';

declare var Stimulsoft: any;
Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OFPaZYasFtsxNoDemsFOXbvf7SIcnyAkFX/4u37NTfx7g+0IqLXw6QIPolr1PvCSZz8Z5wjBNakeCVozGGOiuCOQDy60XNqfbgrOjxgQ5y/u54K4g7R/xuWmpdx5OMAbUbcy3WbhPCbJJYTI5Hg8C/gsbHSnC2EeOCuyA9ImrNyjsUHkLEh9y4WoRw7lRIc1x+dli8jSJxt9C+NYVUIqK7MEeCmmVyFEGN8mNnqZp4vTe98kxAr4dWSmhcQahHGuFBhKQLlVOdlJ/OT+WPX1zS2UmnkTrxun+FWpCC5bLDlwhlslxtyaN9pV3sRLO6KXM88ZkefRrH21DdR+4j79HA7VLTAsebI79t9nMgmXJ5hB1JKcJMUAgWpxT7C7JUGcWCPIG10NuCd9XQ7H4ykQ4Ve6J2LuNo9SbvP6jPwdfQJB6fJBnKg4mtNuLMlQ4pnXDc+wJmqgw25NfHpFmrZYACZOtLEJoPtMWxxwDzZEYYfT";


@Component({
    templateUrl: './herdCertificate.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./herdCertificate.component.less']
})
export class HerdCertificateComponent extends AppComponentBase implements OnInit{

    @ViewChild('herdCombobox', { static: true }) herdCombobox: ElementRef;
    
    herdsSelectItems: SelectItem[] = [];
    herdId: number;
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
        Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OFPaZYasFtsxNoDemsFOXbvf7SIcnyAkFX/4u37NTfx7g+0IqLXw6QIPolr1PvCSZz8Z5wjBNakeCVozGGOiuCOQDy60XNqfbgrOjxgQ5y/u54K4g7R/xuWmpdx5OMAbUbcy3WbhPCbJJYTI5Hg8C/gsbHSnC2EeOCuyA9ImrNyjsUHkLEh9y4WoRw7lRIc1x+dli8jSJxt9C+NYVUIqK7MEeCmmVyFEGN8mNnqZp4vTe98kxAr4dWSmhcQahHGuFBhKQLlVOdlJ/OT+WPX1zS2UmnkTrxun+FWpCC5bLDlwhlslxtyaN9pV3sRLO6KXM88ZkefRrH21DdR+4j79HA7VLTAsebI79t9nMgmXJ5hB1JKcJMUAgWpxT7C7JUGcWCPIG10NuCd9XQ7H4ykQ4Ve6J2LuNo9SbvP6jPwdfQJB6fJBnKg4mtNuLMlQ4pnXDc+wJmqgw25NfHpFmrZYACZOtLEJoPtMWxxwDzZEYYfT";

    }
  
    ngOnInit() {
        this.loadHerdCombo();        
    }

    loadHerdCombo(): void {
        this._herdService.getHerdCertificatedForCombo(true).subscribe(userResult => {

            this.herdsSelectItems = _.map(userResult, function(herd) {
                return {
                    label: herd.displayText, value: Number(herd.value)
                };
            });
        });
    }

    herdCertificate(herdId: number): void {
        this.message.confirm(
            this.l('AreYouSureToConfirmTheHerd'),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._herdService.setHerdCertificated(herdId).subscribe(result => {                         
                        this.notify.info(this.l('SuccessfullySaved'));   
                        this._herdService.getHerdCertificated(herdId).subscribe(result => {
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
                
                            this.viewer.renderHtml('viewer');
                        });

                        this._herdService.getHerdCertificatedForCombo(true).subscribe(userResult => {

                            this.herdsSelectItems = _.map(userResult, function(herd) {
                                return {
                                    label: herd.displayText, value: Number(herd.value)
                                };
                            });
                        });
                    });
                }
            }
        );
    } 
}
