import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BtechComponent } from './btech/btech.component';
import { GditComponent } from './gdit/gdit.component';
import { AwardComponent } from './award/award.component';
import { JarvisComponent } from './jarvis/jarvis.component';
import { OptusComponent } from './optus/optus.component';
import { VimmiComponent } from './vimmi/vimmi.component';
import { ShorComponent } from './shor/shor.component';
import { HeroComponent } from './hero/hero.component';
import { RosterComponent } from './roster/roster.component';
import { PolarisComponent } from './polaris/polaris.component';
import { SigmaComponent } from './sigma/sigma.component';
import { EliteComponent } from './elite/elite.component';
import { ChartoneComponent } from './chartone/chartone.component';
import { CharttwoComponent } from './charttwo/charttwo.component';
import { PolarismenuComponent } from './polarismenu/polarismenu.component';
import { ChartthreeComponent } from './chartthree/chartthree.component';
import { UpdatechartComponent } from './updatechart/updatechart.component';
import { ChartfourComponent } from './chartfour/chartfour.component';
import { ChartfiveComponent } from './chartfive/chartfive.component';
import { ChartsixComponent } from './chartsix/chartsix.component';
import { LandComponent } from './land/land.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    BtechComponent,
    GditComponent,
    AwardComponent,
    JarvisComponent,
    OptusComponent,
    VimmiComponent,
    ShorComponent,
    HeroComponent,
    RosterComponent,
    PolarisComponent,
    SigmaComponent,
    EliteComponent,
     ChartoneComponent,
    CharttwoComponent,
    PolarismenuComponent,
    ChartthreeComponent,
    UpdatechartComponent,
    ChartfourComponent,
    ChartfiveComponent,
    ChartsixComponent,
    LandComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
        { path: 'fetch-data', component: FetchDataComponent },
        { path: 'btech', component: BtechComponent },
        { path: 'gdit', component: GditComponent },
        { path: 'hero', component: HeroComponent },
        { path: 'jarvis', component: JarvisComponent },
        { path: 'optus', component: OptusComponent },
        { path: 'roster', component: RosterComponent },
        { path: 'shor', component: ShorComponent },
        { path: 'vimmi', component: VimmiComponent },
        { path: 'award', component: AwardComponent },
        { path: 'polaris', component: PolarisComponent },
        { path: 'sigma', component: SigmaComponent },
        { path: 'elite', component: EliteComponent },
        { path: 'land', component: LandComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
