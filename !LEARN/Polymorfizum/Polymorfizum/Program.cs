var car = new Car();
var bicycle = new Bicycle();
var boat = new Boat();

Vehicle[] vehicles = [car, bicycle, boat];

foreach (var vehicle in vehicles)
{
    vehicle.Go();
}