
package twiter;

import java.util.Scanner;
public class Twiter {

    public static void main(String[] args) {
        Scanner in=new Scanner(System.in);
       int kindOfSharp;
            System.out.println("choose 1 for rectangle and 2 for triangular or 3 to exit");
       kindOfSharp=in.nextInt();
       while(kindOfSharp==1||kindOfSharp==2){
            System.out.println("enter height and width");
            int height=in.nextInt();
            int width=in.nextInt();
            if(kindOfSharp==1){
                if(height==width||Math.abs(height-width)>5){
                    System.out.println("The area of the square is-"+height*width);
                }
                else{
                     System.out.println("The perimeter of the rectangle is-"+(height*2)+(width*2));
                }
            }
            else{
                    System.out.println("enter 1 for triangle perimeter and 2 for Triangle print"); 
                    kindOfSharp=in.nextInt();
                    if(kindOfSharp==1){
                         double a = Math.sqrt(Math.pow(height, 2) + Math.pow(width/2, 2));
                         double perimeter = (2 * a) + width;
                         System.out.println("Perimeter: " + perimeter);
                    }
                    else{
                        if(width%2==0||width>(height*2)){
                            System.out.println("Error");
                        }
                        if(width%2!=0&&width<(height*2)){
                           int cnt = 0, space=(width/2),i;
                           for ( i = width - 2; i > 1; i = i - 2) {
                                 cnt++;
                           }
                           int numoflevels=cnt;
                           cnt=((height-2)/cnt);
                           for(int m=1;m<=space;m++){
                               System.out.print(" ");
                            }
                            System.out.println("*");
                            for(space=space-1, i=1;i<=((height-2)-(numoflevels*cnt));i++){
                               for(int j=1;j<=space;j++){
                                   System.out.print(" ");
                                }
                                System.out.println("***");
                            }
                            for( i=3;i<=width-2;i+=2,space--){
                                for(int x=1;x<=cnt;x++){
                                   for(int m=1;m<=space;m++){
                                       System.out.print(" ");
                                    }
                                   for(int j=i;j>=1;j--){
                                       System.out.print("*");
                                    }
                                    System.out.println();
                                }
                            }
                            for(int j=width;j>=1;j--){
                                System.out.print("*");
                            }
     
                        }
                    }
            }
            System.out.println("choose 1 for rectangle and 2 for triangular or 3 to exit");
                  kindOfSharp=in.nextInt(); 
       }
       
       
    }
    
}
