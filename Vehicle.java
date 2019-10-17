package vehiclestarter;

 
public class Vehicle {
	private String	manufacturer;
	private String	model;
	private int	makeYear;
	private String regoNumb; 		// TODO add Registration Number 
	private int distanceTraveled;	// TODO add variable for OdometerReading (in KM), 
	private int tankCapacity;       // TODO add variable for TankCapacity (in litres)
               
	private FuelPurchase fuelPurchase;

	/**
	 * Class constructor specifying name of make (manufacturer), model and year
	 * of make.
	   @param manufacturer
	   @param model
 	   @param makeYear
 	   @param regoNumb
 	   @param distanceTraveled
 	   @param tankCapacity
	 */
	public Vehicle(String manufacturer, String model, int makeYear, 
		String regoNumb, int distanceTraveled, int tankCapacity) 

	{
		this.manufacturer = manufacturer;
		this.model = model;
		this.makeYear = makeYear;
		fuelPurchase = new FuelPurchase();
		this.regoNumb = regoNumb;
		this.distanceTraveled = distanceTraveled;
		this.tankCapacity = tankCapacity;

	}

        // TODO Add missing getter and setter methods
        
	/**
	 * Prints details for {@link Vehicle}
	 */
	public void printDetails() {
		System.out.println("Vehicle: \n" + makeYear + " " +
		 manufacturer + " " + model + "\n Registration: " +
		 regoNumb + "\n Tank Capacity: " + TankCapacity +
		 "\n Odometer Reading: " + distanceTraveled);		
        // TODO Display additional information about this vehicle
	}

		//Adds Distanec
	public void AddDistanece(int distance) 
	{
		// TODO Create an addKilometers method which takes a parameter for distance travelled 
        // and adds it to the odometer reading. 

		distanceTraveled = distance + distanceTraveled
	}
        
        

        // adds fuel to the car
        public void addFuel(double litres, double price){            
            fuelPurchase.purchaseFuel(litres, price);
        }        

}
