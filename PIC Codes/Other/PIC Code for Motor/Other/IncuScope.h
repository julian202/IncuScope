#include <18F2455.h>
#device ICD=TRUE
#device adc=8
#FUSES HSPLL                    //HS crystal w/ PLL enabled
#FUSES BROWNOUT                 //Reset when brownout detected
#FUSES BORV20                   //Brownout reset at 2.0V
#FUSES STVREN                   //Stack full/underflow will cause reset
#FUSES noDEBUG                    //Debug mode for use with ICD
#FUSES FCMEN                    //Fail-safe clock monitor enabled
#FUSES NOPBADEN                   //PORTB pins are configured as digital IO on RESET
#FUSES NOEBTR,NOEBTRB,NOCPB,NOCPD,NOWRT,NOWRTB,NOWRTC,NOWRTD,NOWDT,NOPROTECT,NOPUT,NOLVP
#FUSES noMCLR                     //Master Clear pin disabled
#FUSES PLL5                    //Divide By 5(20MHz oscillator input)
#FUSES CPUDIV1                  //System Clock setting 1
#FUSES USBDIV                   //USB clock source comes from PLL divide by 2
#FUSES VREGEN                   //USB voltage regulator enabled

#use delay(clock=48000000)
#include <stdlib.h>
#include <stdio.h>

/////////////////////////////////////////////////////////////////////////////
//In usb.c/h - tells the CCS PIC USB firmware to include HID handling code.
#DEFINE USB_HID_DEVICE  TRUE

//the following defines needed for the CCS USB PIC driver to enable the TX endpoint 1
// and allocate buffer space on the peripheral
#define USB_EP1_TX_ENABLE  USB_ENABLE_INTERRUPT   //turn on EP1 for IN bulk/interrupt transfers
#define USB_EP1_TX_SIZE    64  //allocate 64 bytes in the hardware for transmission   

//the following defines needed for the CCS USB PIC driver to enable the RX endpoint 1
// and allocate buffer space on the peripheral
#define USB_EP1_RX_ENABLE  USB_ENABLE_INTERRUPT   //turn on EP1 for OUT bulk/interrupt transfers
#define USB_EP1_RX_SIZE    64  //allocate 64 bytes in the hardware for reception   

#include <pic18_usb.h>            // Microchip 18Fxx5x hardware layer for usb.c
#include "IncuScope_HIDdesc64.h"  // HID descriptor file
#include <usb.c>                  // handles usb setup tokens and get descriptor reports
 
#define  MOTOR_DRIVE   PIN_A4               // Motor: on = 1 off = 0
#define  MOTOR_DIR     PIN_A5               // Direction: Forward=0 Reverse=1

#define  LED_DF        PIN_B4                // Turn the Darkfield LED on
#define  LED_WHITE     PIN_B5                // Turn the White LED on

#define  LED_RED    PIN_C0                // Turn the Red LED on
#define  LED_GREEN  PIN_C1                // Turn the Green LED on
#define  LED_BLUE   PIN_C2                // Turn the Blue LED on

#define  ALL_LEDs_OFF  output_high(LEDR);output_high(LEDG);output_high(LEDB);output_high(LEDW);output_low(LEDD)




//Global Variables
int8 command[64];      // command received from system (via usb)
int8 CommandList[64] = "F/R/S=Forward/Reverse/Stop\r\nD=DF\r\nW=White\r\n1,2,3=RGB\r\n"; 

