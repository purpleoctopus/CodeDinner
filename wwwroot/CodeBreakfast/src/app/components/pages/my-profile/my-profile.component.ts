import {Component, OnInit} from '@angular/core';
import {UserService} from '../../../services/user.service';
import {HeatMapModule, LegendPosition, PieChartModule} from '@swimlane/ngx-charts';
import {ShortDayOfWeek} from '../../../models/dayofweek';
import {BehaviorSubject, firstValueFrom, map, Observable, Subject, throttleTime} from 'rxjs';
import {AsyncPipe} from '@angular/common';
import {MatIcon} from '@angular/material/icon';
import {ConfigKeySectionVisibility, UserConfigDetailDto, UserConfigKey, UserConfigUpdateDto} from '../../../models/user-config.model';
import {MatIconButton} from '@angular/material/button';
import {UserConfigService} from '../../../services/user-config.service';

@Component({
  selector: 'app-my-profile',
  imports: [
    PieChartModule,
    HeatMapModule,
    AsyncPipe,
    MatIconButton,
    MatIcon,
  ],
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.scss'
})
export class MyProfileComponent implements OnInit {
  private readonly weeksCount = 15;

  private sectionsVisibilityQueue = new Subject<ConfigKeySectionVisibility>();
  protected sectionsVisibility: UserConfigUpdateDto<boolean>[] = [];

  protected courses = [
    {name: 'a', value: 3000},
    {name: 'b', value: 300}
  ]

  private userActivityByDays: BehaviorSubject<any[]>;
  protected userActivityByDays$: Observable<any[]>;

  private userLastActivity: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);
  public userLastActivity$: Observable<any[]> = this.userLastActivity.asObservable();

  constructor(private userService: UserService, private userConfigService: UserConfigService) {
    const arr = [];
    for(let week = 1; week <= this.weeksCount; week++) {
      const dayOfWeekArray = Object.values(ShortDayOfWeek).filter(v => typeof v === 'string');

      const weekData = {
        name: `Week ${week}`,
        series: dayOfWeekArray.map(day => ({
          name: day,
          value: 0
        }))
      }

      arr.push(weekData);
    }
    this.userActivityByDays = new BehaviorSubject<any[]>(arr);
    this.userActivityByDays$ = this.userActivityByDays.asObservable();
  }

  ngOnInit(): void {
    this.sectionsVisibilityQueue.pipe(throttleTime(200)).subscribe(key => {
      this.executeChangeSectionVisibility(key);
    });

    this.userConfigService.getUserConfigs().pipe(map(x=>x.data)).subscribe(configs=>{
      this.sectionsVisibility = configs?.map(x => {
        return {key: x.key, value: x.value === "true"}
      }) ?? [];
    })
  }

  public async ChangeSectionVisibility(key: ConfigKeySectionVisibility){
    this.sectionsVisibilityQueue.next(key);
  }

  private async executeChangeSectionVisibility(key: ConfigKeySectionVisibility) {
    let userConfig = this.sectionsVisibility.find(x => x.key === key);

    if (!userConfig) {
      userConfig = { key, value: true };
    } else {
      userConfig.value = !userConfig.value;
    }

    const res = await firstValueFrom(
      this.userConfigService.updateUserConfigs([{ key: userConfig.key, value: userConfig.value.toString() }])
    );

    if (res.success && res.data) {
      const userConfigs = res.data.map((x: UserConfigDetailDto<boolean | string>) => ({
        ...x,
        value: x.value === "true"
      }));

      this.sectionsVisibility = [...this.sectionsVisibility, ...userConfigs.filter(x => x.key >= 100)];
    }
  }

  public sectionVisibility(key: ConfigKeySectionVisibility){
    return this.sectionsVisibility.find(x=>x.key === key)?.value;
  }

  protected readonly LegendPosition = LegendPosition;
  protected readonly UserConfigKey = UserConfigKey;
}
