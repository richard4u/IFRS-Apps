﻿// Copyright © 2013, 2018, Oracle and/or its affiliates. All rights reserved.
//
// MySQL Connector/NET is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Data;
using System.Globalization;

namespace MySql.Data.MySqlClient.Tests
{
  public class StoredProcedureWithAccess : BaseFixture
  {
    private static string fillError = null;

    public override void SetFixture(SetUpClassPerTestInit fixture)
    {
      fixture.accessToMySqlDb = true;
      base.SetFixture(fixture);
    }

    protected override void Dispose(bool disposing)
    {
      _fixture.execSQL("DROP TABLE IF EXISTS TEST");
      _fixture.execSQL("DROP PROCEDURE IF EXISTS spTest");
      _fixture.execSQL("DROP FUNCTION IF EXISTS fnTest");
      base.Dispose(disposing);
    }

    /// <summary>
    /// Bug #40139	ExecuteNonQuery hangs
    /// </summary>
    [Fact]
    public void CallingStoredProcWithOnlyExecPrivs()
    {
      if (_fixture.Version  < new Version(5, 0)) return;

      _fixture.execSQL("CREATE PROCEDURE spTest() BEGIN SELECT 1; END");
      _fixture.execSQL("CREATE PROCEDURE spTest2() BEGIN SELECT 1; END");
      _fixture.suExecSQL(String.Format("CREATE USER 'abc'@'%' IDENTIFIED BY 'abc'; GRANT USAGE ON `{0}`.* TO 'abc'@'%'", _fixture.database0));

      try
      {
        _fixture.suExecSQL(String.Format("GRANT SELECT ON `{0}`.* TO 'abc'@'%'", _fixture.database0));
        _fixture.suExecSQL(String.Format("GRANT EXECUTE ON PROCEDURE `{0}`.spTest TO abc", _fixture.database0));

        string connStr = _fixture.GetConnectionString("abc", "abc", true) + "; check parameters=false";

        using (MySqlConnection c = new MySqlConnection(connStr))
        {
          c.Open();
          MySqlCommand cmd = new MySqlCommand("spTest", c);
          cmd.CommandType = CommandType.StoredProcedure;
          object o = null;
          Assert.DoesNotThrow(delegate { o = cmd.ExecuteScalar(); });
          Assert.Equal(1, Convert.ToInt32(o));

          cmd.CommandText = "spTest2";
          Assert.Throws(typeof(MySqlException), delegate { cmd.ExecuteScalar(); });
        }
      }
      finally
      {
        _fixture.suExecSQL("DROP USER abc");
      }
    }

    [Fact]
    public void ProcedureParameters()
    {
      if (_fixture.Version  < new Version(5, 0)) return;

      _fixture.execSQL("DROP PROCEDURE IF EXISTS spTest");
      _fixture.execSQL("CREATE PROCEDURE spTest (id int, name varchar(50)) BEGIN SELECT 1; END");

      string[] restrictions = new string[5];
      restrictions[1] = _fixture.database0;
      restrictions[2] = "spTest";
#if RT
      MySqlSchemaCollection dt = st.conn.GetSchemaCollection("Procedure Parameters", restrictions);
      string tableName = dt.Name;
#else
      DataTable dt = _fixture.conn.GetSchema("Procedure Parameters", restrictions);
      string tableName = dt.TableName;
#endif
      Assert.True(dt.Rows.Count == 2);
      Assert.Equal("Procedure Parameters", tableName);
      Assert.Equal(_fixture.database0.ToLower(), dt.Rows[0]["SPECIFIC_SCHEMA"].ToString().ToLower());
      Assert.Equal("sptest", dt.Rows[0]["SPECIFIC_NAME"].ToString().ToLower());
      Assert.Equal("id", dt.Rows[0]["PARAMETER_NAME"].ToString().ToLower());
      Assert.Equal(1, dt.Rows[0]["ORDINAL_POSITION"]);
      Assert.Equal("IN", dt.Rows[0]["PARAMETER_MODE"]);

      restrictions[4] = "name";
#if RT
      dt = st.conn.GetSchemaCollection("Procedure Parameters", restrictions);
#else
      dt.Clear();
      dt = _fixture.conn.GetSchema("Procedure Parameters", restrictions);
#endif
      Assert.Equal(1, dt.Rows.Count);
      Assert.Equal("sptest", dt.Rows[0]["SPECIFIC_NAME"].ToString().ToLower());
      Assert.Equal("name", dt.Rows[0]["PARAMETER_NAME"].ToString().ToLower());
      Assert.Equal(2, dt.Rows[0]["ORDINAL_POSITION"]);
      Assert.Equal("IN", dt.Rows[0]["PARAMETER_MODE"]);

      _fixture.execSQL("DROP FUNCTION IF EXISTS spFunc");
      _fixture.execSQL("CREATE FUNCTION spFunc (id int) RETURNS INT BEGIN RETURN 1; END");

      restrictions[4] = null;
      restrictions[1] = _fixture.database0;
      restrictions[2] = "spFunc";
#if RT
      dt = st.conn.GetSchemaCollection("Procedure Parameters", restrictions);
#else
      dt = _fixture.conn.GetSchema("Procedure Parameters", restrictions);
#endif
      Assert.True(dt.Rows.Count == 2);
      Assert.Equal("Procedure Parameters", tableName);
      Assert.Equal(_fixture.database0.ToLower(), dt.Rows[0]["SPECIFIC_SCHEMA"].ToString().ToLower());
      Assert.Equal("spfunc", dt.Rows[0]["SPECIFIC_NAME"].ToString().ToLower());
      Assert.Equal(0, dt.Rows[0]["ORDINAL_POSITION"]);

      Assert.Equal(_fixture.database0.ToLower(), dt.Rows[1]["SPECIFIC_SCHEMA"].ToString().ToLower());
      Assert.Equal("spfunc", dt.Rows[1]["SPECIFIC_NAME"].ToString().ToLower());
      Assert.Equal("id", dt.Rows[1]["PARAMETER_NAME"].ToString().ToLower());
      Assert.Equal(1, dt.Rows[1]["ORDINAL_POSITION"]);
      Assert.Equal("IN", dt.Rows[1]["PARAMETER_MODE"]);
    }

    [Fact]
    public void SingleProcedureParameters()
    {
      if (_fixture.Version  < new Version(5, 0)) return;

      _fixture.execSQL("DROP PROCEDURE IF EXISTS spTest");
      _fixture.execSQL("CREATE PROCEDURE spTest(id int, IN id2 INT(11), " +
          "INOUT io1 VARCHAR(20), OUT out1 FLOAT) BEGIN END");
      string[] restrictions = new string[4];
      restrictions[1] = _fixture.database0;
      restrictions[2] = "spTest";
#if RT
      MySqlSchemaCollection procs = st.conn.GetSchemaCollection("PROCEDURES", restrictions);
#else
      DataTable procs = _fixture.conn.GetSchema("PROCEDURES", restrictions);
#endif
      Assert.Equal(1, procs.Rows.Count);
      Assert.Equal("spTest", procs.Rows[0][0]);
      Assert.Equal(_fixture.database0.ToLower(), procs.Rows[0][2].ToString().ToLower(CultureInfo.InvariantCulture));
      Assert.Equal("spTest", procs.Rows[0][3]);

#if RT
      MySqlSchemaCollection parameters = st.conn.GetSchemaCollection("PROCEDURE PARAMETERS", restrictions);
#else
      DataTable parameters = _fixture.conn.GetSchema("PROCEDURE PARAMETERS", restrictions);
#endif
      Assert.Equal(4, parameters.Rows.Count);

#if RT
      MySqlSchemaRow row = parameters.Rows[0];
#else
      DataRow row = parameters.Rows[0];
#endif
      Assert.Equal(_fixture.database0.ToLower(CultureInfo.InvariantCulture),
          row["SPECIFIC_SCHEMA"].ToString().ToLower(CultureInfo.InvariantCulture));
      Assert.Equal("spTest", row["SPECIFIC_NAME"]);
      Assert.Equal(1, row["ORDINAL_POSITION"]);
      Assert.Equal("IN", row["PARAMETER_MODE"]);
      Assert.Equal("id", row["PARAMETER_NAME"]);
      Assert.Equal("INT", row["DATA_TYPE"].ToString().ToUpper(CultureInfo.InvariantCulture));

      row = parameters.Rows[1];
      Assert.Equal(_fixture.database0.ToLower(CultureInfo.InvariantCulture), row["SPECIFIC_SCHEMA"].ToString().ToLower(CultureInfo.InvariantCulture));
      Assert.Equal("spTest", row["SPECIFIC_NAME"]);
      Assert.Equal(2, row["ORDINAL_POSITION"]);
      Assert.Equal("IN", row["PARAMETER_MODE"]);
      Assert.Equal("id2", row["PARAMETER_NAME"]);
      Assert.Equal("INT", row["DATA_TYPE"].ToString().ToUpper(CultureInfo.InvariantCulture));

      row = parameters.Rows[2];
      Assert.Equal(_fixture.database0.ToLower(CultureInfo.InvariantCulture), row["SPECIFIC_SCHEMA"].ToString().ToLower(CultureInfo.InvariantCulture));
      Assert.Equal("spTest", row["SPECIFIC_NAME"]);
      Assert.Equal(3, row["ORDINAL_POSITION"]);
      Assert.Equal("INOUT", row["PARAMETER_MODE"]);
      Assert.Equal("io1", row["PARAMETER_NAME"]);
      Assert.Equal("VARCHAR", row["DATA_TYPE"].ToString().ToUpper(CultureInfo.InvariantCulture));

      row = parameters.Rows[3];
      Assert.Equal(_fixture.database0.ToLower(CultureInfo.InvariantCulture), row["SPECIFIC_SCHEMA"].ToString().ToLower(CultureInfo.InvariantCulture));
      Assert.Equal("spTest", row["SPECIFIC_NAME"]);
      Assert.Equal(4, row["ORDINAL_POSITION"]);
      Assert.Equal("OUT", row["PARAMETER_MODE"]);
      Assert.Equal("out1", row["PARAMETER_NAME"]);
      Assert.Equal("FLOAT", row["DATA_TYPE"].ToString().ToUpper(CultureInfo.InvariantCulture));
    }

#if !RT
    /// <summary>
    /// Bug #27679  	MySqlCommandBuilder.DeriveParameters ignores UNSIGNED flag
    /// </summary>
    [Fact]
    public void UnsignedParametersInSP()
    {
      if (_fixture.Version  < new Version(5, 0)) return;

      _fixture.execSQL("CREATE PROCEDURE spTest(testid TINYINT UNSIGNED) BEGIN SELECT testid; END");

      MySqlCommand cmd = new MySqlCommand("spTest", _fixture.conn);
      cmd.CommandType = CommandType.StoredProcedure;
      MySqlCommandBuilder.DeriveParameters(cmd);
      Assert.Equal(MySqlDbType.UByte, cmd.Parameters[0].MySqlDbType);
      Assert.Equal(DbType.Byte, cmd.Parameters[0].DbType);
    }

    [Fact]
    public void CheckNameOfReturnParameter()
    {
      if (_fixture.Version  < new Version(5, 0)) return;

      _fixture.execSQL("DROP FUNCTION IF EXISTS fnTest");
      _fixture.execSQL("CREATE FUNCTION fnTest() RETURNS CHAR(50)" +
          " LANGUAGE SQL DETERMINISTIC BEGIN  RETURN \"Test\"; END");

      MySqlCommand cmd = new MySqlCommand("fnTest", _fixture.conn);
      cmd.CommandType = CommandType.StoredProcedure;
      MySqlCommandBuilder.DeriveParameters(cmd);
      Assert.Equal(1, cmd.Parameters.Count);
      Assert.Equal("@RETURN_VALUE", cmd.Parameters[0].ParameterName);
    }

    /// <summary>
    /// Bug #13632  	the MySQLCommandBuilder.deriveparameters has not been updated for MySQL 5
    /// Bug #15077  	Error MySqlCommandBuilder.DeriveParameters for sp without parameters.
    /// Bug #19515  	DiscoverParameters fails on numeric datatype
    /// </summary>
    [Fact]
    public void DeriveParameters()
    {
      if (_fixture.Version  < new Version(5, 0)) return;

      _fixture.execSQL("CREATE TABLE test2 (c CHAR(20))");
      _fixture.execSQL("INSERT INTO test2 values ( 'xxxx')");
      MySqlCommand cmd2 = new MySqlCommand("SELECT * FROM test2", _fixture.conn);
      using (MySqlDataReader reader = cmd2.ExecuteReader())
      {
      }

      _fixture.execSQL("CREATE PROCEDURE spTest(IN \r\nvalin DECIMAL(10,2), " +
          "\nIN val2 INT, INOUT val3 FLOAT, OUT val4 DOUBLE, INOUT val5 BIT, " +
          "val6 VARCHAR(155), val7 SET('a','b'), val8 CHAR, val9 NUMERIC(10,2)) " +
               "BEGIN SELECT 1; END");

      MySqlCommand cmd = new MySqlCommand("spTest", _fixture.conn);
      cmd.CommandType = CommandType.StoredProcedure;
      MySqlCommandBuilder.DeriveParameters(cmd);

      Assert.Equal(9, cmd.Parameters.Count);
      Assert.Equal("@valin", cmd.Parameters[0].ParameterName);
      Assert.Equal(ParameterDirection.Input, cmd.Parameters[0].Direction);
      Assert.Equal(MySqlDbType.NewDecimal, cmd.Parameters[0].MySqlDbType);

      Assert.Equal("@val2", cmd.Parameters[1].ParameterName);
      Assert.Equal(ParameterDirection.Input, cmd.Parameters[1].Direction);
      Assert.Equal(MySqlDbType.Int32, cmd.Parameters[1].MySqlDbType);

      Assert.Equal("@val3", cmd.Parameters[2].ParameterName);
      Assert.Equal(ParameterDirection.InputOutput, cmd.Parameters[2].Direction);
      Assert.Equal(MySqlDbType.Float, cmd.Parameters[2].MySqlDbType);

      Assert.Equal("@val4", cmd.Parameters[3].ParameterName);
      Assert.Equal(ParameterDirection.Output, cmd.Parameters[3].Direction);
      Assert.Equal(MySqlDbType.Double, cmd.Parameters[3].MySqlDbType);

      Assert.Equal("@val5", cmd.Parameters[4].ParameterName);
      Assert.Equal(ParameterDirection.InputOutput, cmd.Parameters[4].Direction);
      Assert.Equal(MySqlDbType.Bit, cmd.Parameters[4].MySqlDbType);

      Assert.Equal("@val6", cmd.Parameters[5].ParameterName);
      Assert.Equal(ParameterDirection.Input, cmd.Parameters[5].Direction);
      Assert.Equal(MySqlDbType.VarChar, cmd.Parameters[5].MySqlDbType);

      Assert.Equal("@val7", cmd.Parameters[6].ParameterName);
      Assert.Equal(ParameterDirection.Input, cmd.Parameters[6].Direction);
      Assert.Equal(MySqlDbType.Set, cmd.Parameters[6].MySqlDbType);

      Assert.Equal("@val8", cmd.Parameters[7].ParameterName);
      Assert.Equal(ParameterDirection.Input, cmd.Parameters[7].Direction);
      Assert.Equal(MySqlDbType.String, cmd.Parameters[7].MySqlDbType);

      Assert.Equal("@val9", cmd.Parameters[8].ParameterName);
      Assert.Equal(ParameterDirection.Input, cmd.Parameters[8].Direction);
      Assert.Equal(MySqlDbType.NewDecimal, cmd.Parameters[8].MySqlDbType);

      _fixture.execSQL("DROP PROCEDURE spTest");
      _fixture.execSQL("CREATE PROCEDURE spTest() BEGIN END");
      cmd.CommandText = "spTest";
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Clear();
      MySqlCommandBuilder.DeriveParameters(cmd);
      Assert.Equal(0, cmd.Parameters.Count);
    }

    /// <summary>
    /// Bug #13632  	the MySQLCommandBuilder.deriveparameters has not been updated for MySQL 5
    /// </summary>
    [Fact]
    public void DeriveParametersForFunction()
    {
      if (_fixture.Version  < new Version(5, 0)) return;
      
      _fixture.execSQL("DROP FUNCTION IF EXISTS fnTest");
      _fixture.execSQL("CREATE FUNCTION fnTest(v1 DATETIME) RETURNS INT " +
          "  LANGUAGE SQL DETERMINISTIC BEGIN RETURN 1; END");

      MySqlCommand cmd = new MySqlCommand("fnTest", _fixture.conn);
      cmd.CommandType = CommandType.StoredProcedure;
      MySqlCommandBuilder.DeriveParameters(cmd);

      Assert.Equal(2, cmd.Parameters.Count);
      Assert.Equal("@v1", cmd.Parameters[1].ParameterName);
      Assert.Equal(ParameterDirection.Input, cmd.Parameters[1].Direction);
      Assert.Equal(MySqlDbType.DateTime, cmd.Parameters[1].MySqlDbType);

      Assert.Equal(ParameterDirection.ReturnValue, cmd.Parameters[0].Direction);
      Assert.Equal(MySqlDbType.Int32, cmd.Parameters[0].MySqlDbType);
    }

    /// <summary> 
    /// Bug #49642	FormatException when returning empty string from a stored function 
    /// </summary> 
    [Fact]
    public void NotSpecifyingDataTypeOfReturnValue()
    {
      if (_fixture.Version  < new Version(5, 0)) return;

      _fixture.execSQL("DROP FUNCTION IF EXISTS TestFunction");

      _fixture.execSQL(@"CREATE FUNCTION `TestFunction`() 
                RETURNS varchar(20) 
                RETURN ''");
      MySqlCommand cmd = new MySqlCommand("TestFunction", _fixture.conn);
      cmd.CommandType = CommandType.StoredProcedure;
      MySqlParameter returnParam = new MySqlParameter();
      returnParam.ParameterName = "?RetVal_";
      returnParam.Direction = ParameterDirection.ReturnValue;
      cmd.Parameters.Add(returnParam);
      cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Bug #50123	Batch updates bug when UpdateBatchSize > 1
    /// Bug #50444	Parameters.Clear() not working
    /// </summary>
    [Fact]
    public void UpdateBatchSizeMoreThanOne()
    {      
      _fixture.execSQL(@"CREATE TABLE test(fldID INT NOT NULL, 
                fldValue VARCHAR(50) NOT NULL, PRIMARY KEY(fldID))");

      MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM test", _fixture.conn);
      DataTable data = new DataTable();
      adapter.Fill(data);

      MySqlCommand ins = new MySqlCommand(
        "INSERT INTO test(fldID, fldValue) VALUES (?p1, ?p2)", _fixture.conn);
      ins.Parameters.Add("p1", MySqlDbType.Int32).SourceColumn = "fldID";
      ins.Parameters.Add("p2", MySqlDbType.String).SourceColumn = "fldValue";

      ins.UpdatedRowSource = UpdateRowSource.None;
      adapter.InsertCommand = ins;
      adapter.UpdateBatchSize = 10;

      int numToInsert = 20;
      for (int i = 0; i < numToInsert; i++)
      {
        DataRow row = data.NewRow();
        row["fldID"] = i + 1;
        row["fldValue"] = "ID = " + (i + 1);
        data.Rows.Add(row);
      }
      Assert.Equal(numToInsert, adapter.Update(data));

      //UPDATE VIA SP
      MySqlCommand comm = new MySqlCommand("DROP PROCEDURE IF EXISTS pbug50123", _fixture.conn);
      comm.ExecuteNonQuery();
      comm.CommandText = "CREATE PROCEDURE pbug50123(" +
          "IN pfldID INT, IN pfldValue VARCHAR(50)) " +
          "BEGIN INSERT INTO test(fldID, fldValue) " +
          "VALUES(pfldID, pfldValue); END";
      comm.ExecuteNonQuery();

      // Set the Insert Command
      ins.Parameters.Clear();
      ins.CommandText = "pbug50123";
      ins.CommandType = CommandType.StoredProcedure;
      ins.Parameters.Add("pfldID", MySqlDbType.Int32).SourceColumn = "fldID";
      ins.Parameters.Add("pfldValue", MySqlDbType.String).SourceColumn = "fldValue";
      ins.UpdatedRowSource = UpdateRowSource.None;

      for (int i = 21; i < 41; i++)
      {
        DataRow row = data.NewRow();
        row["fldID"] = i + 1;
        row["fldValue"] = "ID = " + (i + 1);
        data.Rows.Add(row);
      }
      // Do the update
      Assert.Equal(numToInsert, adapter.Update(data));
    }
#endif
  }
}
