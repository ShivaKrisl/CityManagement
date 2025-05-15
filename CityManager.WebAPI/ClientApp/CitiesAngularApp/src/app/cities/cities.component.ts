import { Component } from '@angular/core';
import { City } from '../models/city';
import { CitiesService } from '../services/cities.service';
import { CommonModule, NgFor } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DisableControlDirective } from '../directives/disable-control.directive';

@Component({
  selector: 'app-cities',
  imports: [CommonModule, NgFor, HttpClientModule, ReactiveFormsModule, DisableControlDirective],
  templateUrl: './cities.component.html',
  styleUrl: './cities.component.css',
  standalone: true,
  providers: [CitiesService]
})
export class CitiesComponent {
  cities: City[] = [];
  postCityForm: FormGroup;
  isPostCityFormSubmitted: boolean = false;

  putCityForm: FormGroup;
  editCityId: string | null = null;

  constructor(private citiesService: CitiesService) {

    this.postCityForm = new FormGroup({
      cityName: new FormControl(null, [Validators.required])
    });

    this.putCityForm = new FormGroup({
      cities: new FormArray([])
    });

  };

  get putCityFormArray(): FormArray {
    return this.putCityForm.get('cities') as FormArray;
  }

  loadCities() {
    this.citiesService.getCities()
      .subscribe({
        next: (response: City[]) => {
          this.cities = response;
          console.log(this.cities);
          this.cities.forEach(city => {
            this.putCityFormArray.push(new FormGroup({
              cityId: new FormControl(city.cityId, [Validators.required]),
              cityName: new FormControl(city.cityName, [Validators.required])
            }));
          });
        },
        error: (error: any) => { console.log(error); },
        complete: () => { }
      });
  }


  ngOnInit() {
    this.loadCities();
  }



  get postCity_CityNameControl(): any {
    return this.postCityForm.get('cityName');
  }

  public postCitySubmitted() {
    // Todo
    this.isPostCityFormSubmitted = true;
    //console.log(this.postCityForm.value);

    this.citiesService.AddCity(this.postCityForm.value).subscribe({
      next: (response: City) => {
        console.log(response);
        this.cities.push(new City(response.cityId, response.cityName));
        this.putCityFormArray.push(new FormGroup({
          cityId: new FormControl(response.cityId, [Validators.required]),
          cityName: new FormControl({ value: response.cityName, disabled : true }, [Validators.required])
        }));
      },
      error: (error: any) => { console.log(error); },
      complete: () => {
        this.isPostCityFormSubmitted = false;
        this.postCityForm.reset();
        //this.loadCities();
      }
    });
  }

  public editClicked(city: City) : void {
    this.editCityId = city.cityId;
    console.log(city.cityName);
  }

  public updateClicked(i: number): void {

    this.citiesService.UpdateCity(this.putCityFormArray.controls[i].value).subscribe({
      next: (response: City) => {
        console.log(response);
        this.editCityId = null;
        this.putCityFormArray.controls[i].reset(this.putCityFormArray.controls[i].value);

      },
      error: (error: any) => { console.log(error); },
      complete: () => { }
    });

  }

  public deleteCity(city: City, i: number): void {
    if (confirm(`Are you sure you want to delete this city: ${city.cityName}?`)) {


      this.citiesService.DeleteCity(city.cityId).subscribe({
        next: (response: string) => {
          //this.cities = this.cities.filter(city => city.cityId !== cityId);

          this.putCityFormArray.removeAt(i);
          this.cities.splice(i, 1);

        },
        error: (error: any) => { console.log(error); },
        complete: () => { }
      });

    }
  }

}
