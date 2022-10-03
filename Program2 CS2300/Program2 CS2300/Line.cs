using System;

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
    float[] startCoord = new float[2];
    float[] endCoord = new float[2];
    float[] parametricV = new float[2];

    float slope;
    float absoluteValueOfSlope;
    float b;

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
    /// The V for a parametric equation
    /// </summary>
    public float[] ParametricV
    {
        get { return parametricV; }
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
        // Set start and end points, making sure the line goes from left to right
        
        if (startColumn <= endColumn)
        {
            this.startRow = startRow - 1;       //P1
            this.startColumn = startColumn - 1; //P2
            this.endRow = endRow - 1;           //Q1
            this.endColumn = endColumn - 1;     //Q2
        } 
        else
        {
            this.startRow = endRow - 1;         //P1
            this.startColumn = endColumn - 1;   //P2
            this.endRow = startRow - 1;         //Q1
            this.endColumn = startColumn - 1;   //Q2
        }

        // Set the matrix coord of the start and end of the line
        this.startCoord[0] = this.startRow;
        this.startCoord[1] = this.endRow;

        // Set the V value for the parametric equation of the line
        this.parametricV[0] = this.endRow - this.startRow; 
        this.parametricV[1] = this.endColumn - this.startColumn;

        // Set the absolute value of the slope if denominator is not 0
        if (this.endColumn - this.startColumn != 0)
        {
            // Set slope
            this.slope = (this.endRow - this.startRow) / (this.endColumn - this.startColumn);

            // Set absolute value of slope
            this.absoluteValueOfSlope = Math.Abs(this.slope);
        }
        // If denom = 0 then set to -1 
        else
        {
            this.absoluteValueOfSlope = -1;
        }

        // Set b
        b = this.startRow - slope * this.startColumn;
	}

    /// <summary>
    /// Find the column point for the given row point
    /// </summary>
    /// <param name="rowPoint">known row point</param>
    /// <returns>corresponding column point</returns>
    public float FindColumnPoint (float rowPoint)
    {
        float columnPoint =  (rowPoint - this.b) / this.slope;
        return columnPoint;
    }

    /// <summary>
    /// Find the row point for the given column point
    /// </summary>
    /// <param name="columnPoint">known column point</param>
    /// <returns>correspoinding row point</returns>
    public float FindRowPoint (float columnPoint)
    {
        float rowPoint = this.slope * columnPoint + this.b;
        return rowPoint;
    }

    #endregion
}
