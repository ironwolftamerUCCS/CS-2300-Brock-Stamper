using System;
using static System.Formats.Asn1.AsnWriter;

/// <summary>
/// A line
/// </summary>
public class Line
{
    #region Fields

    float startRow;
    float startColumn;
    float endRow;
    float endColumn;

    float slope;
    float absoluteValueOfSlope;

    #endregion

    #region Properties

    /// <summary>
    /// Row the line starts at
    /// </summary>
    public float StartRow
    {
        get { return startRow; }
    }

    /// <summary>
    /// Column the line starts at
    /// </summary>
    public float StartColumn
    {
        get { return startColumn; }
    }

    /// <summary>
    /// Row the line ends at
    /// </summary>
    public float EndRow
    {
        get { return endRow; }
    }
    
    /// <summary>
    /// Column the line ends at
    /// </summary>
    public float EndColumn
    {
        get { return endColumn; }
    }

    /// <summary>
    /// Slope of the line
    /// </summary>
    public float Slope
    {
        get { return slope; }
    }

    /// <summary>
    /// Slope of the line
    /// </summary>
    public float AbsoluteValueOfSlope
    {
        get { return absoluteValueOfSlope; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Line class constructor
    /// </summary>
    /// <param name="startRow"></param>
    /// <param name="startColumn"></param>
    /// <param name="endRow"></param>
    /// <param name="endColumn"></param>
    public Line(float startRow, float startColumn, float endRow, float endColumn)
	{
        // Set start and end points
        this.startRow = startRow;
        this.startColumn = startColumn;
        this.endRow = endRow;
        this.endColumn = endColumn;

        // Set the absolute value of the slope if denominator is not 0
        if (endColumn - startColumn != 0)
        {
            // Set slope
            slope = (endRow - startRow) / (endColumn - startColumn);

            // Set absolute value of slope
            absoluteValueOfSlope = Math.Abs(slope);
        }
        // If denom = 0 then set to -1 
        else
        {
            absoluteValueOfSlope = -1;
        }
	}

    #endregion
}
