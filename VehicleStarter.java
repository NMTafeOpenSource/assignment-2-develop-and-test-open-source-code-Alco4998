/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
// package vehiclestarter;
import vehiclestarter.*;
import java.util.Random;

/**
 * Starter code for Vehicle application. 
 * This class displays sample output to the console.
 * @author AUTHORNAME
 */
public class VehicleStarter {

    /** Main entry point to the program
     * @param args the command line arguments
     */

    public static void main(String[] args) {        
        Vehicle v = new Vehicle("Ford", "T812", 2014, "1frg829", 10000, 314159);
		
		// Vehicle sample distance
            System.out.print("\n");
            v.addFuel(new Random().nextDouble()*10, 1.3);

            v.printDetails();                
            System.out.print("\n");
    }
    
}
