/********************************************************************************
IncuScope.c 
W. Zipfel, Tae Cooke                         V2.0                   10/2011

IncuScope  PIC code command set: 
Query command:  q - Returns a string which lists state of all LEDs
All LEDS off:  a  
Step motor forward for n cycles:   fn,  where n = 0 to 255
Step motor in reverse for n cycles:   rn, where n = 0 to 255
Toggle dark field illuminator ON/OFF:  d           
Toggle white LED ON/OFF :  w       
Toggle red LED ON/OFF :  1 
Toggle green LED ON/OFF :  2
Toggle blue LED ON/OFF :  3       

******************************************************************************/
#include <18F2455.h>
#fuses HSPLL,NOWDT, NOPROTECT,NOLVP,NODEBUG,USBDIV,PLL5,CPUDIV1,VREGEN, MCLR
#use delay(clock=48000000)
#include <stdlib.h>

// In usb.c/h - tells the CCS PIC USB firmware to include HID handling code.
#DEFINE USB_HID_DEVICE  TRUE
#define USB_EP1_TX_ENABLE  USB_ENABLE_INTERRUPT   //turn on EP1 for IN bulk/interrupt transfers
#define USB_EP1_TX_SIZE    64  //allocate 64 bytes in the hardware for transmission   

#define USB_EP1_RX_ENABLE  USB_ENABLE_INTERRUPT   //turn on EP1 for OUT bulk/interrupt transfers
#define USB_EP1_RX_SIZE    64    // allocate 64 bytes in the hardware for reception   

#include <pic18_usb.h>            // Microchip 18Fxx5x hardware layer for usb.c
#include "IncuScope_HIDdesc64.h"  // HID descriptor file
#include <usb.c>                  // handles usb setup tokens and get descriptor reports

#define  MOTOR_DRIVE   PIN_A4                // Motor: on = 1 off = 0
#define  MOTOR_DIR     PIN_A5 

#define  STEP1         PIN_B7               // Direction: Forward=0 Reverse=1
#define  DIRECTION1    PIN_B5
#define  ENABLE1       PIN_B4 

#define  STEP2         PIN_B3
#define  DIRECTION2    PIN_B2  
#define  ENABLE2       PIN_B1 

#define  LED_GREEN     PIN_C1                // Turn the Green LED on
#define  LED_BLUE      PIN_C0 




              // Turn the Blue LED on

//#define  MOTOR_DELAY       10                // number of ms to delay per for loop

// prototypes
void ExecuteCmd(void); 
void pulse(void);

//Global Variables
int16 delay;
int8 command[64];      // command received from system (via usb)
int8 LEDStatus;        // byte that holds LED on/off status

int16 i;
int16 j;
// bit 0 = Dark Field
// bit 1 = white LED
// bit 2 = Red LED
// bit 3 = Green LED
// bit 4 = blue LED
// the rest are unused for now

void main()
{
 //TurnLEDsOff();
 usb_init();               // initialize usb communication
 setup_adc_ports(NO_ANALOGS);
 setup_spi(FALSE);
 setup_timer_0(RTCC_INTERNAL);
 setup_timer_1(T1_DISABLED);
 setup_timer_2(T2_DISABLED,0,1);
 setup_comparator(NC_NC_NC_NC);
 setup_vref(FALSE);
 setup_oscillator(False);
 delay=4;
//delay=20;

output_high(DIRECTION1); //direction1
output_high(DIRECTION2); //direction2
//output_low(PIN_B6);
output_low(ENABLE2);//enable switch chip1
output_low(ENABLE1);//enable switch chip2
output_low(STEP1); 
output_low(STEP2); 

 while (TRUE) {
 //output_low(STEP1); 

  //while (TRUE) {  
 
      //output_high(PIN_B6);
    
      //output_high(STEP1);
      //delay_ms(delay);
      //output_low(STEP1);
      //delay_ms(delay);
      
      
     
      
      
      if (usb_enumerated()) {         
          if (usb_kbhit(1)) {   // Check for USB commands
              usb_gets(1, command, 64, 100);
              ExecuteCmd();
              
          }
      }
      
    //}
   //output_low(PIN_B6);
   
 //  if (usb_enumerated()) {         
  //     if (usb_kbhit(1)) {   // Check for USB commands
    //       usb_gets(1, command, 64, 100);
    //       ExecuteCmd();
           
 
     //    }
  // }
 }
}



//   Executes a command based on the 1st byte in the HUD string 
void ExecuteCmd(void) 
{
 int8 n; 
 
 
 
 
  switch (command[0]) {
  
  
  
  
     case 0://UP
      output_low(DIRECTION1);
      output_low(DIRECTION2);
          break;  

     case 1://DOWN
      output_high(DIRECTION1);
      output_high(DIRECTION2);
          break;  
       
       default:
  }
  
  for (i=0; i<command[2]; i++){
              
              
           for (j=0; j<command[1]; j++){
                       pulse();
               }
  }



  
}
void pulse(void)
{


  switch (command[3]) {

    case 0://motor1
      output_high(STEP1);
      delay_ms(delay);
      output_low(STEP1);
      delay_ms(delay);
          break;  

     case 1://motor2
      output_high(STEP2);
      delay_ms(delay);
      output_low(STEP2);
      delay_ms(delay);
          break;  
       
       default:
  }


}
  

