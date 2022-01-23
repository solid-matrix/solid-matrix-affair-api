namespace SolidMatrix.Affair.Api.Core;

public enum ExceptionCode
{
    Success = 0,

    Unknown = 50,
    ModelInValid = 100,
    NotFound = 101,

    UniqueConstraint = 201,
    CannotInsertNull = 202,
    MaxLengthExceeded = 203,
    NumericOverflow = 204,
    ReferenceConstraint = 205,

}