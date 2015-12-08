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
#define  MOTOR_DIR     PIN_A5                // Direction: Forward=0 Reverse=1
#define  LED_DF        PIN_B4                // Turn the Darkfield LED on
#define  LED_WHITE     PIN_B5                // Turn the White LED on
#define  LED_RED       PIN_C0                // Turn the Red LED on
#define  LED_GREEN     PIN_C1                // Turn the Green LED on
#define  LED_BLUE      PIN_C2                // Turn the Blue LED on
#define  MOTOR_DELAY       10                // number of ms to delay per for loop

// prototypes
void ExecuteCmd(void); 
void DriveMotor(int8 n);
void TurnLEDsOff(void);  

//Global Variables
int8 command[64];      // command received from system (via usb)
int8 LEDStatus;        // byte that holds LED on/off status
// bit 0 = Dark Field
// bit 1 = white LED
// bit 2 = Red LED
// bit 3 = Green LED
// bit 4 = blue LED
// the rest are unused for now

void main()
{
 TurnLEDsOff();
 usb_init();               // initialize usb communication
 setup_adc_ports(NO_ANALOGS);
 setup_spi(FALSE);
 setup_timer_0(RTCC_INTERNAL);
 setup_timer_1(T1_DISABLED);
 setup_timer_2(T2_DISABLED,0,1);
 setup_comparator(NC_NC_NC_NC);
 setup_vref(FALSE);
 setup_oscillator(False);
      
 while (TRUE) {
   if (usb_enumerated()) {         
       if (usb_kbhit(1)) {   // Check for USB commands
           usb_gets(1, command, 64, 100);
           ExecuteCmd();
       }
   }
 }
}

//   Executes a command based on the 1st byte in the HUD string 
void ExecuteCmd(void) 
{
 int8 n; 
 
 switch (command[0]) {  
       case 'q':
          Command[0] = LEDStatus;  // loads LED on/off status into the command buffer for return
          usb_puts(1, command, 64, 100);
          break;
      
      case 'a':
          TurnLEDsOff(); 
          usb_puts(1, command, 64, 100);
          break; 
      
      case 'f':
          n = (int8) command[1]; 
          usb_puts(1, command, 64, 100);
          output_low(MOTOR_DIR);    // set direction to forward
          DriveMotor(n); 
          break;
               
      case 'r':
          n = (int8) command[1];
          usb_puts(1, command, 64, 100);
          output_high(MOTOR_DIR);    // set direction to reverse
          DriveMotor(n);
          break; 
                     
      case 'd':  // note DF is set low to turn off 
           if(bit_test(LEDStatus, 0)) { 
              output_low(LED_DF);
              bit_clear(LEDStatus, 0); 
           }
           else {
              output_high(LED_DF);
              bit_set(LEDStatus, 0);
           }
           usb_puts(1, command, 64, 100);
           break;
             
      case 'w':  // w, 1,2, and 3 are all off when high
           if(bit_test(LEDStatus, 1)) { 
              output_high(LED_WHITE);
              bit_clear(LEDStatus, 1); 
           }
           else {
              output_low(LED_WHITE);
              bit_set(LEDStatus, 1);
           }
           usb_puts(1, command, 64, 100);
           break;
             
      case '1':
           if(bit_test(LEDStatus, 2)) { 
              output_high(LED_RED);
              bit_clear(LEDStatus, 2); 
           }
           else {
              output_low(LED_RED);
              bit_set(LEDStatus, 2);
           }
           usb_puts(1, command, 64, 100);
           break;
      
      case '2':
           if(bit_test(LEDStatus, 3)) { 
              output_high(LED_GREEN);
              bit_clear(LEDStatus, 3); 
           }
           else {
              output_low(LED_GREEN);
              bit_set(LEDStatus, 3);
           }
           usb_puts(1, command, 64, 100);
           break;
      
      case '3':
           if(bit_test(LEDStatus, 4)) { 
              output_high(LED_BLUE);
              bit_clear(LEDStatus, 4); 
           }
           else {
              output_low(LED_BLUE);
              bit_set(LEDStatus, 4);
           }
           usb_puts(1, command, 64, 100);
          break;
       
      default:
         
   }
}

  
// Turns all LEDS off
void  TurnLEDsOff(void) 
{
  output_high(LED_RED);
  output_high(LED_GREEN);
  output_high(LED_BLUE);
  output_high(LED_WHITE);
  output_low(LED_DF);
  LEDStatus = 0x00; 
}

void DriveMotor(int8 n)
{
 int8 i; 
 
 if(n > 1) {
    output_high(MOTOR_DRIVE); // turn motor on
    for(i=0;i<n;i++) delay_ms(MOTOR_DELAY);  // waste time
    output_low(MOTOR_DRIVE);
    return;
 }
          
 if(n == 0) 
    output_low(MOTOR_DRIVE);    // turn motor off
 else // n must == 1 
    output_high(MOTOR_DRIVE);   // turn motor on
}
          
