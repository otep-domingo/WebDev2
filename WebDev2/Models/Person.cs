using System.ComponentModel.DataAnnotations; //What is the missing code to make sure annotations are allowed.

namespace WebDev2.Models
{
    public class Person
    {
        private int age;//declare the age private.
        private string? status; 
        public int ID { get; set; }
        [Required]      //What is the annotation that makes the Name property required.
        public string Name { get; set; }    //Make the Name property be able to set value and return the value.
        [Required]      //What is the annotation that makes the Age property required.
        public int Age {
            set { age=value; }
        }
        public string Status
        {
            get { 
                if(age>8 && age <= 12) //set the condition of age to be between 8 and 12
                {
                    status = "Elementary";
                }else if(age>12 && age <= 16) //set the condition of age to be between 13 and 16
                {
                    status = "Highschool";
                }else if(age>16 && age <= 25) //set the condition of age to be between 17 and 25
                {
                    status = "College";
                }
                else
                {
                    status = "Invalid";
                }
                return status; }
        }

    }
}
