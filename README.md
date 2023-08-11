# Danish CPR Number Handling in C#

## Introduction

This project provides a single class, `CPRnr`, that allows you to work with Danish CPR numbers. The class provides methods to process and extract information from CPR numbers, including retrieving the birthdate, checking modolus 11 validity, and determining the gender associated with a CPR number.

## Class: `CPRnr`

The `CPRnr` class provides the following methods:

### `public CPRnr(string cprNumber|long cprNumber)`

This constructor initializes a new instance of the CPRnr class with the provided CPR number. The constructor handles CPR numbers with and without leading zeros and dashes as a string or as a long type. It ensures that the input CPR number is in a recognizable format.

**Parameters:**

- `cprNumber`: The CPR number to be processed. Accepts numbers with and without leading 0 and/or dash.

**Exceptions:**

- `Exception`: Thrown when the provided CPR number is not in a recognizable format.
- `Exception`: Thrown when the provided CPR number cannot be converted into a valid birthday date.

### `public string CPRtrimmed()`

This method returns the trimmed version of the CPR number (i.e. without a dash or leading 0).

### `public string CPRnoDash()`

This method returns the CPR number without a dash, but with leading 0.

### `public string CPRwithDash()`

This method returns the CPR number with a dash and leading 0, this is the perferred way to display CPR numbers.

### `public DateTime GetBirthday()`

This method retrieves the birthdate associated with the CPR number as a `DateTime` object.

**Returns:**

- A `DateTime` object representing the birthdate extracted from the CPR number.

### `public bool DoesPassModulus()`

This method determines whether the CPR number passes the modulus 11 test, which is used to check the validity of the CPR number.  
**Note:** 
_that this check is only to be used on people born before 2007. Read more at [cpr.dk](https://cpr.dk/cpr-systemet/personnumre-uden-kontrolciffer-modulus-11-kontrol)_

**Returns:**

- `true` if the CPR number passes the modulus 11 test, `false` otherwise.

### `public bool IsMale()`

This method determines the gender associated with the CPR number.

**Returns:**

- `true` if the gender is male, `false` if it's female.

## Usage

You can use the `CPRnr` class to work with Danish CPR numbers as follows:

```csharp
// Initialize a CPRnr object with a CPR number (string input)
CPRnr cpr1 = new CPRnr("010203-1234");

// Initialize a CPRnr object with a CPR number (long input)
CPRnr cpr2 = new CPRnr(0102031234);

// Get the trimmed CPR number
string trimmedCPR = cpr1.CPRtrimmed();

// Get the CPR number without dashes
string cprNoDash = cpr1.CPRnoDash();

// Get the CPR number with dashes
string cprWithDash = cpr1.CPRwithDash();

// Get the birthdate associated with the CPR number
DateTime birthdate = cpr1.GetBirthday();

// Check if the CPR number passes the modulus 11 test
bool isValid = cpr1.DoesPassModulus();

// Determine the gender associated with the CPR number
bool isMale = cpr1.IsMale();
```

## License
This project is licensed under the MIT License. Feel free to use and modify the code as needed.
