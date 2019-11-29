![alt text](https://github.com/miroiu/dw-lang/blob/master/DwLang/icon.ico "DwLang logo")
#  DwLang
An educational interpreted programming language that works with big numbers.

## Usage from the command line
dwlang -?

## Specifications:
  - interpreted language
  - execution is single threaded and sequential, all statements are executed in the order they are encountered
  
Language rules: 
  - statements are delimited by a semicolon (i.e. ";")
  - the decimal separator is comma (i.e. ",")
  - empty statements are permitted and ignored during evaluation
  - a statement can be written on a single or multiple lines
  - spaces between an operator and its operands are optional

Data types:
  - a numeric type (BigDecimal)

Variables:
  - variable declarations consists of the 'var' keyword followed by the variable's name
  - variables name consist exclusively of letters and/or underscore
  - variables declaration and initialization can be optionally compound in a single statement (e.g. var a; a = 5; or var a = 5;)
  - reserved words of the language can't be used as variable names
  - reserved words are: var, sqr, pow, x, prm, avg, med, pwd, set, precision, print, cls

Binary operators:
  - \+ -> addition (e.g 2 + 3 = 5)
  - \- -> subtraction (e.g 2 - 1 = 1)
  - : -> division (e.g 6 : 2 = 3)
  - x -> multiplication (e.g 3 x 2 = 6)
  - pow -> power (e.g 2 pow 3 = 8)
  - prm (permutations without repetitions where n is the number of things to choose
from and we choose r of them, no repetitions, order matters. "n prm r" should evaluate to n!/(n − r)! )
  - pwd (number of possible passwords of maximum n characters, made on an alphabet of t
characters. How many passwords of maximum 8 characters can I make using exclusively English letters: that is "52 pwd 8" -> which should be equal to 52 pow 8 + 52 pow 7 + ..... + 52 pow 1)

Unary operators:
  - sqr -> square root (e.g sqr 81 = 9)
  - ! -> factorial (e.g 4! = 24)
  - \- (for negative numbers, e.g. -1)

Commands:
  - print (prints to the output stream, e.g. print 2 x 3)
  - cls (clears the output stream)

Var args operators:
  - avg -> average (e.g. avg 2 3 7 = 4)
  - med -> median (e.g. med 2 4 7 8 9 = 7)

Operations grouping:
  - parentheses with unlimited nesting depth

Directives:
  - set precision <n> (e.g. <n> can be variable or integer: "set precision 4; print 7/6;" should print 1,1667) can be set multiple times in the same program

Errors:
  - syntax errors and evaluation errors are printed to the console with their respective line and column numbers
  - if an operation has a non-deterministic result, the program should crash (e.g. a non-terminated decimal
expansion)
  - attempting to use a variable that was not assigned any value should throw an evaluation error
  - evaluation and syntax errors are not fail-fast, meaning that the code will execute until an error is found

Comments:
  - anything sourounded by /* and *\ is ignored during evaluation 
  - if enclosing markers are not balanced, the most outer ones are considered (e.g. "/* some comment *\ print 5; *\ print 3;" should only print 3.)
